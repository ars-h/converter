using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.CompareTables.Command
{
    
    public class CompareTablesCommand : IRequest<Result>
    {
        public int byColumn { get; set; }
    }

    public class CompareTablesCommandHandler : IRequestHandler<CompareTablesCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public CompareTablesCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(CompareTablesCommand request, CancellationToken cancellationToken)
        {
            if (request.byColumn == 1)
            {
                PropertyInfo[] properties = typeof(Table1).GetProperties();
                
                await foreach (var t1 in _context.Table1)
                {
                    //left
                    if (_context.Table2.All(t2 => properties[request.byColumn].GetValue(t2)
                                                  != properties[request.byColumn].GetValue(t1)))
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
                    if (_context.Table1.All(t1 => properties[request.byColumn].GetValue(t1)
                                                  != properties[request.byColumn].GetValue(t2)))
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

                _context.SaveChangesAsync(cancellationToken);
            }
            return Result.Success();

        }
        
        public static bool compareByColumn(int column)
        {
            if (column == 1)
            {
                
            }

            return true;
        }
    }
    
    
    
}