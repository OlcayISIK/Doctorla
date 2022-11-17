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
using Doctorla.Data.Entities.DailyChecking;
using Doctorla.Data.Entities.Doctor;
using Doctorla.Data.Entities.Region;
using Doctorla.Data.Entities.SystemAppointments;
using Doctorla.Data.Entities.SystemPackages;
using Doctorla.Data.Entities.SystemUsers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        public DbSet<UserType> UserType { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<RelUserDepartment> RelUserDepartment { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<RoleGroup> RoleGroup { get; set; }
        public DbSet<Referance> Referance { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuType> MenuType { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<County> County { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<AddressType> AddressType { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Sick> Sick { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<AppointmentFiles> AppointmentFiles { get; set; }
        public DbSet<AppointmentProcess> AppointmentProcess { get; set; }
        public DbSet<UserDetail> UserDetail { get; set; }
        public DbSet<DoctorDetail> DoctorDetail { get; set; }
        public DbSet<DoctorExperiences> DoctorExperiences { get; set; }
        public DbSet<DoctorMedicalInterests> DoctorMedicalInterests { get; set; }
        public DbSet<DoctorScientificMembership> DoctorScientificMembership { get; set; }
        public DbSet<DoctorEducations> DoctorEducations { get; set; }
        public DbSet<Donations> Donations { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<DailyCheck> DailyCheck { get; set; }
        public DbSet<DailyCheckDetail> DailyCheckDetail { get; set; }
        public DbSet<DailyCheckDetailValues> DailyCheckDetailValues { get; set; }
        public DbSet<DailyCheckPackages> DailyCheckPackages { get; set; }
        public DbSet<DailyCheckPackagesValues> DailyCheckPackagesValues { get; set; }
        public DbSet<PaymentProcess> PaymentProcess { get; set; }
        public DbSet<PackageChat> PackageChat { get; set; }
        public DbSet<DailyCheckAlerts> DailyCheckAlerts { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Package> Package { get; set; }
        public DbSet<PackageDetail> PackageDetail { get; set; }
        public DbSet<PackageOffers> PackageOffers { get; set; }
        public DbSet<RelPackageDetail> RelPackageDetail { get; set; }
        public DbSet<Campaign> Campaign { get; set; }
        public DbSet<UsedCampaign> UsedCampaign { get; set; }
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
