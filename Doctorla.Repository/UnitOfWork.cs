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

        public UnitOfWork(Context context, IRedisTokenRepository refreshTokens,
            IAdminRepository admins, IUserRepository users,
            IHospitalRepository hospitals, IDoctorRepository doctors)
        {
            _context = context;
            RedisTokens = refreshTokens;
            Admins = admins;
            Users = users;
            Hospitals = hospitals;
            Doctors = doctors;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> Commit()
        {
            return await _context.SaveChangesAsync();
        }
        public long HospitalId => _context.HospitalId;

        public DateTime Now => _context.Now;


    }
}

