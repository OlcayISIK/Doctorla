using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Core.Enums
{
    public enum AppointmentStatus
    {
        Active,
        Requested,
        Approved,
        InProgress,
        Ended,

        Deactive,
        CanceledByDoctor,
        CanceledByUser,
        CanceledByAdmin
    }
}
