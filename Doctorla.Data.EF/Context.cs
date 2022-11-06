using Doctorla.Core;
using Doctorla.Core.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Doctorla.Data;
using System.Threading.Tasks;
using Doctorla.Data.Entities;

namespace Doctorla.Data.EF
{
    public class Context : DbContext
    {
        public readonly DateTime Now;
        public readonly long HospitalId;
        private readonly string _connectionString;
        private readonly bool _constructedManually = false;

        #region Entities
        public DbSet<Admin> Admins { get; set; }
        #endregion
        public Context(string connectionString)
        {
            Now = DateTime.UtcNow;
            _constructedManually = true;
            _connectionString = connectionString;
        }

        public Context(DbContextOptions options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var httpContextAccessor = (IHttpContextAccessor)ServiceLocator.ResolveService(typeof(IHttpContextAccessor));
            var claims = ClaimUtils.GetClaims(httpContextAccessor?.HttpContext?.User?.Claims);
            HospitalId = claims.HospitalId;
            Now = DateTime.UtcNow;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_constructedManually)
                optionsBuilder.UseSqlServer(_connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Owned<MultiString>();

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
