using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Core.Enums
{
    public enum AppointmentStatus
    {
        Created = 1,
        Approved = 2,

        CanceledByDoctor = 3,
        CanceledByUser = 4,
        CanceledByAdmin = 5,
    }
}
