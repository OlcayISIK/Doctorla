using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Core.Enums
{
    public enum PaymentStatus
    {
        Error = 0,
        PaymentRedirect = 1,
        Success = 2,
        PaymentFailed = 3
    }
}
