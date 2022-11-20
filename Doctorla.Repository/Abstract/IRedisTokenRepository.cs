using Doctorla.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Repository.Abstract
{
    public interface IRedisTokenRepository
    {
        Task Set(RedisToken token, int expireInMinutes);
        Task<RedisToken> Get(string key);
        Task Remove(string key);
    }
}
