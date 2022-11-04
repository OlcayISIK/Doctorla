using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Core.Enums
{
    public enum ErrorCode
    {
        InvalidUsernameOrPassword = 101,
        InvalidRefreshToken = 102,
        InvalidPasswordResetToken = 103,
        InvalidAction = 104,
        CannotDeleteAccountWithReservation = 105,

        PermissionDenied = 206,

        InternalServerError = 500,
    }
}
