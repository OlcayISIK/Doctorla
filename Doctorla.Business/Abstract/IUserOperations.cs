using Doctorla.Dto.Members.DoctorEntity;
using Doctorla.Dto.Members.Profile;
using Doctorla.Dto.Members;
using Doctorla.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Business.Abstract
{
    public interface IUserOperations
    {
        #region Shared
        Task<Result<IEnumerable<UserDto>>> GetAll();
        Task<Result<UserDto>> Get(long? userId = null);
        #endregion

        #region User
        Task<Result<bool>> UpdateForUser(UserDto userDto);
        Task<Result<bool>> ChangePassword(ChangePasswordDto changePasswordDto);
        #endregion

        #region Admin
        Task<Result<bool>> Add(UserDto userDto);
        Task<Result<bool>> UpdateForAdmin(UserDto userDto);
        Task<Result<bool>> Delete(long id);
        #endregion
    }
}
