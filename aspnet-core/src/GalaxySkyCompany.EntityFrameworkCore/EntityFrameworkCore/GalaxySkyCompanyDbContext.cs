using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using GalaxySkyCompany.Authorization.Roles;
using GalaxySkyCompany.Authorization.Users;
using GalaxySkyCompany.MultiTenancy;
using GalaxySkyCompany.Models;

namespace GalaxySkyCompany.EntityFrameworkCore
{
    public class GalaxySkyCompanyDbContext : AbpZeroDbContext<Tenant, Role, User, GalaxySkyCompanyDbContext>
    {
        public GalaxySkyCompanyDbContext(DbContextOptions<GalaxySkyCompanyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Airport> Airports { get; set; }

        public virtual DbSet<Pilot> Pilots { get; set; }

        public virtual DbSet<Plane> Planes { get; set; }

        public virtual DbSet<PilotPlane> PilotPlanes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PilotPlane>().HasKey(pp => new { pp.PilotId, pp.PlaneId });
            modelBuilder.Entity<PilotPlane>().HasOne(pp => pp.Pilot).WithMany(p => p.PilotPlanes).IsRequired().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PilotPlane>().HasOne(pp => pp.Plane).WithMany(p => p.PilotPlanes).IsRequired().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
