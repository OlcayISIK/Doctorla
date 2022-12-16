using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Core.Enums
{
    public enum AccountStatus
    {
        Created = 0,

        // User confirmed
        Approved = 1,

        Suspended = 2,

        Deleted = 3
    }
}
