using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Repository.Abstract
{
    public interface IUnitOfWork
    {
        DateTime Now { get; }
        long HospitalId { get; }
        Task<int> Commit();

        IRedisTokenRepository RedisTokens { get; }
        IAdminRepository Admins { get; }

    }
}
