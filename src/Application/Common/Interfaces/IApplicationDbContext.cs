using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TodoList> TodoLists { get; set; }

        DbSet<TodoItem> TodoItems { get; set; }
        DbSet<Table1> Table1 { get; set; }
        DbSet<Table2> Table2 { get; set; }
        
         DbSet<Left> Left { get; set; }
         DbSet<Right> Right { get; set; }
         DbSet<Inner> Inner { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
