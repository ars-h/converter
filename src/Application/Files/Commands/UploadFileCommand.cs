using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities.UploadedFile;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Table = CleanArchitecture.Domain.Entities.Table;

namespace CleanArchitecture.Application.Files.Commands
{
    public record UploadFileCommand : IRequest<Result>
    {
        public UploadedFile file1 { get; set; }
        public UploadedFile file2 { get; set; }
        public int byColumn { get; set; }
    }

    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public UploadFileCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Byte[] bytes1 = Convert.FromBase64String(request.file1.fileBase64.Split(',').Last());
                Byte[] bytes2 = Convert.FromBase64String(request.file2.fileBase64.Split(',').Last());
                string file1Path =
                    $"../Application/Files/TempFiles/{DateTime.UtcNow.Second}_{DateTime.UtcNow.Millisecond}_{request.file1.name}";
                string file2Path =
                    $"../Application/Files/TempFiles/{DateTime.UtcNow.Second}_{DateTime.UtcNow.Millisecond}_{request.file2.name}";
                File.WriteAllBytes(file1Path, bytes1);
                File.WriteAllBytes(file2Path, bytes2);
                
                await foreach (var t in _context.Table1)
                {
                    _context.Table1.Remove(t);
                }
                
                await foreach (var t in _context.Table2)
                {
                    _context.Table2.Remove(t);
                }
                
                await foreach (var t in _context.Left)
                {
                    _context.Left.Remove(t);
                }
                
                await foreach (var t in _context.Right)
                {
                    _context.Right.Remove(t);
                }
                
                await foreach (var t in _context.Inner)
                {
                    _context.Inner.Remove(t);
                }

                writeExcel(file1Path,1,_context);
                await _context.SaveChangesAsync(cancellationToken);
                writeExcel(file2Path,2,_context);
                DeleteTempFiles("../Application/Files/TempFiles/");
                await _context.SaveChangesAsync(cancellationToken);
                compare(request.byColumn,_context);
                await _context.SaveChangesAsync(cancellationToken);

                
                return Result.Success();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Result.Failure(new[] {"Error"});
                
            }
        }


   

        static void writeExcel(string fileName,int tableNumber,IApplicationDbContext context)
        {
            using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(fileName, true))
            {
                WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
                IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>()
                    .Elements<Sheet>();
                string relationshipId = sheets.First().Id.Value;
                WorksheetPart worksheetPart =
                    (WorksheetPart) spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
                Worksheet workSheet = worksheetPart.Worksheet;
                SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                Row[] rows = sheetData.Descendants<Row>().ToArray();

               
                for (int j = 1; j < rows.Count(); j++)
                {
                    Console.WriteLine($"Tox - {j} | Table - {tableNumber}");
                    Table1 newRow1 = new Table1();
                    Table2 newRow2 = new Table2();
                    PropertyInfo[] properties = typeof(Table1).GetProperties();
                    
                    for (int i = 0; i < rows[j].Descendants<Cell>().Count(); i++)
                    {
                        var element = GetCellValue(spreadSheetDocument, rows[j]?.Descendants<Cell>()?.ElementAt(i));
                        
                        
                        if (tableNumber == 1)
                        {
                            properties[i+1].SetValue(newRow1,element);
                        }
                        else if (tableNumber == 2)
                        {
                            properties[i+1].SetValue(newRow2,element);  
                        }
                    }

                    if (tableNumber == 1)
                    {
                        context.Table1.Add(newRow1);
                    }
                    else if (tableNumber == 2)
                    {
                        context.Table2.Add(newRow2);
                    }
                }
                
            }
        }

        private static string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
            string value = cell?.CellValue?.InnerXml;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
            else
            {
                return value;
            }
        }

        private static void DeleteTempFiles(string path)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }

            Console.WriteLine("Temp files deleted . . .");
        }


        private async  static void  compare(int byColumn,IApplicationDbContext _context)
        {

            Console.WriteLine("Compare started . . .");
            PropertyInfo[] properties = typeof(Table1).GetProperties();
            string queryStringLeft =
                $"SELECT t1.Id,t1.field1,t1.field2,t1.field3,t1.field4,t1.field5,t1.field6,t1.field7,t1.field8,t1.field9,t1.field10 FROM table1 t1 LEFT JOIN table2 t2 ON t2.{properties[byColumn].Name} = t1.{properties[byColumn].Name} WHERE t2.{properties[byColumn].Name} IS NULL";
            string queryStringRight =
                $"SELECT t2.Id,t2.field1,t2.field2,t2.field3,t2.field4,t2.field5,t2.field6,t2.field7,t2.field8,t2.field9,t2.field10 FROM table2 t2 LEFT JOIN table1 t1 ON t1.{properties[byColumn].Name} = t2.{properties[byColumn].Name} WHERE t1.{properties[byColumn].Name} IS NULL";
            string queryStringInner =
                $"SELECT t1.Id,t1.field1,t1.field2,t1.field3,t1.field4,t1.field5,t1.field6,t1.field7,t1.field8,t1.field9,t1.field10 FROM table1 t1 LEFT JOIN table2 t2 ON t2.{properties[byColumn].Name} = t1.{properties[byColumn].Name} WHERE t2.{properties[byColumn].Name} IS NOT NULL";

            Console.WriteLine(queryStringInner);
            Console.WriteLine(queryStringLeft);
            var leftItems = _context.Table1.FromSqlRaw(queryStringLeft).ToList();
            var rightItems = _context.Table2.FromSqlRaw(queryStringRight).ToList();
            var innerItems = _context.Table1.FromSqlRaw(queryStringInner).ToList();
            
            
            foreach (var leftItem in leftItems)
            {
                _context.Left.Add(new Left()
                {
                    field1 =  leftItem.field1,
                    field2 =  leftItem.field2,
                    field3 =  leftItem.field3,
                    field4 =  leftItem.field4,
                    field5 =  leftItem.field5,
                    field6 =  leftItem.field6,
                    field7 =  leftItem.field7,
                    field8 =  leftItem.field8,
                    field9 =  leftItem.field9,
                    field10 = leftItem.field10,
                });
            }
            
            foreach (var rightItem in rightItems)
            {
                _context.Right.Add(new Right()
                {
                    field1 =  rightItem.field1,
                    field2 =  rightItem.field2,
                    field3 =  rightItem.field3,
                    field4 =  rightItem.field4,
                    field5 =  rightItem.field5,
                    field6 =  rightItem.field6,
                    field7 =  rightItem.field7,
                    field8 =  rightItem.field8,
                    field9 =  rightItem.field9,
                    field10 = rightItem.field10,
                });
            }
            
            foreach (var innerItem in innerItems)
            {
                _context.Inner.Add(new Inner()
                {
                    field1 =  innerItem.field1,
                    field2 =  innerItem.field2,
                    field3 =  innerItem.field3,
                    field4 =  innerItem.field4,
                    field5 =  innerItem.field5,
                    field6 =  innerItem.field6,
                    field7 =  innerItem.field7,
                    field8 =  innerItem.field8,
                    field9 =  innerItem.field9,
                    field10 = innerItem.field10,
                });
            }
            
            
            /*
             await foreach (var t1 in _context.Table1)
             {
                 var valuetest = properties[byColumn].GetValue(t1);
                     //left
                     if (_context.Table2.All(t2 => properties[byColumn].GetValue(t2)
                                                   != properties[byColumn].GetValue(t1)))
                     {
                         _context.Left.Add(new Left()
                         {
                             field1 = t1.field1,
                             field2 = t1.field2,
                             field3 = t1.field3,
                             field4 = t1.field4,
                             field5 = t1.field5,
                             field6 = t1.field6,
                             field7 = t1.field7,
                             field8 = t1.field8,
                             field9 = t1.field9,
                             field10 = t1.field10,
                         });
                     }
                     else
                     {
                         _context.Inner.Add(new Inner()
                         {
                             field1 = t1.field1,
                             field2 = t1.field2,
                             field3 = t1.field3,
                             field4 = t1.field4,
                             field5 = t1.field5,
                             field6 = t1.field6,
                             field7 = t1.field7,
                             field8 = t1.field8,
                             field9 = t1.field9,
                             field10 = t1.field10,
                         });
                     }
 
                 }
                 
                 await foreach (var t2 in _context.Table2)
                 {
                     //right
                     if (_context.Table1.All(t1 => properties[byColumn].GetValue(t1)
                                                   != properties[byColumn].GetValue(t2)))
                     {
                         _context.Right.Add(new Right()
                         {
                             field1 = t2.field1,
                             field2 = t2.field2,
                             field3 = t2.field3,
                             field4 = t2.field4,
                             field5 = t2.field5,
                             field6 = t2.field6,
                             field7 = t2.field7,
                             field8 = t2.field8,
                             field9 = t2.field9,
                             field10 = t2.field10,
                         });
                     }
 
                 }
                 */


        }
        
    }
}
