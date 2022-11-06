using Doctorla.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities
{
    /// <summary>
    /// This will be kept in redis as a short term entity, so no inheriting from <see cref="Entity"/>
    /// </summary>
    public class RedisToken
    {
        public string TokenValue { get; set; }
        public long UserId { get; set; }
        public string Username { get; set; }
        public ApiConsumerType ConsumerType { get; set; }
        public RedisTokenType TokenType { get; set; }
        public bool CanSeeHiddenCompanies { get; set; }
    }
}
