using Microsoft.EntityFrameworkCore;
using Licensing.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Licensing.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<LicenseApplication> Applications { get; }
    DbSet<Document> Documents { get; }
    DbSet<Feedback> Feedbacks { get; }
    DbSet<ApplicationSnapshot> ApplicationSnapshots { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
