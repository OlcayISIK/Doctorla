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
using Doctorla.Data.Entities.SystemPackages;
using Doctorla.Data.Entities.SystemUsers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Doctorla.Data.Entities.SystemAppoinments;
using Doctorla.Data.Admins;
using Doctorla.Data.Hospitals;
using Doctorla.Data.Doctors;

namespace Doctorla.Data.EF
{
    public class Context : DbContext
    {
        public readonly DateTime Now;
        //public readonly long HospitalId;
        private readonly string _connectionString;
        private readonly bool _constructedManually = false;

        //#region Entities
        //public DbSet<Admin> Admins { get; set; }
        //public DbSet<UserType> UserTypes { get; set; }
        //public DbSet<User> Users { get; set; }
        //public DbSet<RelUserDepartment> RelUserDepartments { get; set; }
        //public DbSet<Department> Departments { get; set; }
        //public DbSet<RoleGroup> RoleGroups { get; set; }
        //public DbSet<Referance> Referances { get; set; }
        //public DbSet<Menu> Menus { get; set; }
        //public DbSet<MenuType> MenuTypes { get; set; }
        //public DbSet<Country> Countries { get; set; }
        //public DbSet<City> Cities { get; set; }
        //public DbSet<County> Counties { get; set; }
        //public DbSet<Address> Addresses { get; set; }
        //public DbSet<AddressType> AddressTypes { get; set; }
        //public DbSet<Contact> Contacts { get; set; }
        //public DbSet<Sick> Sicks { get; set; }
        //public DbSet<Appointment> Appointments { get; set; }
        //public DbSet<AppointmentFiles> AppointmentFiles { get; set; }
        //public DbSet<AppointmentProcess> AppointmentProcesses { get; set; }
        //public DbSet<UserDetail> UserDetails { get; set; }
        //public DbSet<DoctorDetail> DoctorDetails { get; set; }
        //public DbSet<DoctorExperiences> DoctorExperiences { get; set; }
        //public DbSet<DoctorMedicalInterests> DoctorMedicalInterests { get; set; }
        //public DbSet<DoctorScientificMembership> DoctorScientificMemberships { get; set; }
        //public DbSet<DoctorEducations> DoctorEducations { get; set; }
        //public DbSet<Donations> Donations { get; set; }
        //public DbSet<ErrorLog> ErrorLogs { get; set; }
        //public DbSet<DailyCheck> DailyChecks { get; set; }
        //public DbSet<DailyCheckDetail> DailyCheckDetails { get; set; }
        //public DbSet<DailyCheckDetailValues> DailyCheckDetailValues { get; set; }
        //public DbSet<DailyCheckPackages> DailyCheckPackages { get; set; }
        //public DbSet<DailyCheckPackagesValues> DailyCheckPackageValues { get; set; }
        //public DbSet<PaymentProcess> PaymentProcesses { get; set; }
        //public DbSet<PackageChat> PackageChats { get; set; }
        //public DbSet<DailyCheckAlerts> DailyCheckAlerts { get; set; }
        //public DbSet<Post> Posts { get; set; }
        //public DbSet<Blog> Blogs { get; set; }
        //public DbSet<Package> Packages { get; set; }
        //public DbSet<PackageDetail> PackageDetails { get; set; }
        //public DbSet<PackageOffers> PackageOffers { get; set; }
        //public DbSet<RelPackageDetail> RelPackageDetails { get; set; }
        //public DbSet<Campaign> Campaigns { get; set; }
        //public DbSet<UsedCampaign> UsedCampaigns { get; set; }
        //#endregion

        #region Entity
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
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
            //var httpContextAccessor = (IHttpContextAccessor)ServiceLocator.ResolveService(typeof(IHttpContextAccessor));
            //var claims = ClaimUtils.GetClaims(httpContextAccessor?.HttpContext?.User?.Claims);
            //HospitalId = claims.HospitalId;
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
