using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Core.Enums
{
    public enum ApiConsumerType
    {
        /// <summary>
        /// End users of the doctorla, consumers of the web and mobile API's
        /// </summary>
        Doctor = 0,

        /// <summary>
        /// Normal People
        /// </summary>
        User = 1,

        /// <summary>
        /// Administrators of the system, Doctorla staff 
        /// </summary>
        Admin = 2,

        /// <summary>
        /// End users of the hospitals, consumers of the web API's
        /// </summary>
        Hospital = 3
    }
}
