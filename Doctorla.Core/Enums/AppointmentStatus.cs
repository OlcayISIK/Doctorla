using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Core.Enums
{
    public enum AppointmentStatus
    {
        Active = 1,
        Requested = 2,
        Approved = 3,
        InProgress = 4,
        Ended = 5,

        Deactive = 9,
        CanceledByDoctor = 10,
        CanceledByUser = 11,
        CanceledByAdmin = 12,
    }
}
