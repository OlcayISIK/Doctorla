using Doctorla.Data.EF;
using Doctorla.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Repository
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;

        public IRedisTokenRepository RedisTokens { get; }
        public IAdminRepository Admins { get; }
        public IUserRepository Users { get; }
        public IHospitalRepository Hospitals { get; }
        public IDoctorRepository Doctors { get; }
        public IAppointmentRepository Appointments { get; }
        public ISpecialtyRepository Specialties { get; }
        public IBlogPostRepository BlogPosts { get; }
        public IDoctorEducationsRepository DoctorEducations { get; }
        public IDoctorExperiencesRepository DoctorExperiences { get; }
        public IDoctorMedicalInterestsRepository DoctorMedicalInterests { get; }
        public IDoctorScientificMembershipsRepository DoctorScientificMemberships { get; }

        public UnitOfWork(Context context, IRedisTokenRepository refreshTokens,
            IAdminRepository admins, IUserRepository users,
            IHospitalRepository hospitals, IDoctorRepository doctors,
            IAppointmentRepository appointments, ISpecialtyRepository specialties,
            IBlogPostRepository blogPosts, IDoctorEducationsRepository doctorEducations,
            IDoctorExperiencesRepository doctorExperiences, IDoctorMedicalInterestsRepository doctorMedicalInterests,
            IDoctorScientificMembershipsRepository doctorScientificMemberships)
        {
            _context = context;
            RedisTokens = refreshTokens;
            Admins = admins;
            Users = users;
            Hospitals = hospitals;
            Doctors = doctors;
            Appointments = appointments;
            Specialties = specialties;
            BlogPosts = blogPosts;
            DoctorEducations = doctorEducations;
            DoctorExperiences = doctorExperiences;
            DoctorMedicalInterests = doctorMedicalInterests;
            DoctorScientificMemberships = doctorScientificMemberships;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> Commit()
        {
            return await _context.SaveChangesAsync();
        }
        //public long HospitalId => _context.HospitalId;

        public DateTime Now => _context.Now;
    }
}

