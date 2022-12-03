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
    public interface IBlogPostOperations
    {
        #region Shared
        Task<Result<IEnumerable<BlogPostDto>>> GetAll();
        #endregion

        #region Admin
        Task<Result<bool>> Add(BlogPostDto blogPostDtoDto);
        Task<Result<bool>> Update(BlogPostDto blogPostDtoDto);
        Task<Result<bool>> Delete(long id);
        #endregion
    }
}
