using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Core.Enums
{
    public enum ErrorCode
    {
        InvalidEmailOrPassword = 101,
        InvalidRefreshToken = 102,
        InvalidPasswordResetToken = 103,
        InvalidAction = 104,

        PermissionDenied = 206,

        ObjectNotFound = 404,
        ObjectAlreadyExists = 405,

        InternalServerError = 500,
    }
}
