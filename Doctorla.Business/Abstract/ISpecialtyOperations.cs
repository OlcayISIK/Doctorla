using Doctorla.Data.Entities.SystemAppoinments;
using Doctorla.Dto;
using Doctorla.Dto.Auth;
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
        #endregion

        #region Admin
        Task<Result<bool>> Add(SpecialtyDto specialtyDto);
        Task<Result<bool>> Update(SpecialtyDto specialtyDto);
        Task<Result<bool>> Delete(long id);
        #endregion
    }
}
