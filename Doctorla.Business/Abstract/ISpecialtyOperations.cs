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
    public interface ISpecialtyOperations
    {
        #region Shared
        Task<Result<IEnumerable<SpecialtyDto>>> GetAll();
        Task<Result<SpecialtyDto>> Get(long id);
        Task<Result<IEnumerable<DoctorDto>>> GetDoctorWithSpecialities(long id);
        #endregion

        #region Admin
        Task<Result<bool>> Add(SpecialtyDto specialtyDto);
        Task<Result<bool>> Update(SpecialtyDto specialtyDto);
        Task<Result<bool>> Delete(long id);
        #endregion
    }
}
