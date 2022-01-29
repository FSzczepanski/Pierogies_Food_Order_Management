using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces
{
    using SystemSettings = Domain.Entities.SystemSettings;

    public interface IApplicationDbContext
    {
        DbSet<User> MyUsers { get; set; }
        DbSet<Position> Positions { get; set; }
        DbSet<Form> Forms { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<SystemSettings> SystemSettings { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
