using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Core.Enums
{
    public enum RedisTokenType
    {
        RefreshToken = 0,
        PasswordResetToken = 1,
        UserApprovalToken = 2
    }
}
