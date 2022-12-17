using Doctorla.Data.Entities.SystemAppoinments;
using Doctorla.Dto;
using Doctorla.Dto.Auth;
using Doctorla.Dto.Members;
using Doctorla.Dto.Members.DoctorEntity;
using Doctorla.Dto.Shared;
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
        Task<Result<IEnumerable<DoctorPreviewDto>>> GetAllAvailableInGivenDate(DateTime date);
        #endregion

        #region Doctor
        Task<Result<bool>> UpdateFoDoctor(DoctorDto doctorDto);
        #endregion

        #region Admin
        Task<Result<bool>> Add(DoctorDto doctorDto);
        Task<Result<bool>> UpdateForAdmin(DoctorDto doctorDto);
        Task<Result<bool>> Delete(long id);
        #endregion
    }
}
