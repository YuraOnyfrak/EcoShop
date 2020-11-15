using EcoShop.Entrepreneur.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EcoShop.Entrepreneur.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<DocumentProvingQuality> Documents { get; set; }
    }
}
