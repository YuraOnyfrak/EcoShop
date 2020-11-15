using EcoShop.Entrepreneur.Application.Common.Interfaces;
using EcoShop.Entrepreneur.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EcoShop.Entrepreneur.Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);           
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<DocumentProvingQuality>  Documents { get; set; }
    }
}
