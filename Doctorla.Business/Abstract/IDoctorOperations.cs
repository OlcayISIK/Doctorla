using Doctorla.Business.Helpers;
using Doctorla.Core.Enums;
using Doctorla.Core.Utils;
using Doctorla.Data.Entities.SystemAppoinments;
using Doctorla.Dto;
using Doctorla.Dto.Auth;
using Doctorla.Dto.Members;
using Doctorla.Dto.Members.DoctorEntity;
using Doctorla.Dto.Members.Profile;
using Doctorla.Dto.Shared;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Business.Abstract
{
    public interface IDoctorOperations
    {
        #region Shared
        Task<Result<IEnumerable<DoctorPreviewDto>>> GetAll();
        Task<Result<DoctorDto>> Get(long? doctodId = null);
        Task<Result<DoctorDetailsDto>> GetDoctorDetails(long? doctodId = null);
        //Task<Result<IEnumerable<DoctorPreviewDto>>> GetAllAvailableInGivenDate(DateTime date);
        #endregion

        #region Doctor
        Task<Result<bool>> UpdateForDoctor(DoctorDto doctorDto);
        Task<Result<bool>> ChangePassword(ChangePasswordDto changePasswordDto);
        #endregion

        #region Admin
        Task<Result<bool>> Add(DoctorDto doctorDto);
        Task<Result<bool>> UpdateForAdmin(DoctorDto doctorDto);
        Task<Result<bool>> Delete(long id);
        #endregion
    }
}
