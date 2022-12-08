using Doctorla.Data.Entities.SystemAppoinments;
using Doctorla.Dto;
using Doctorla.Dto.Auth;
using Doctorla.Dto.Shared.Blog;
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
        Task<Result<IEnumerable<BlogPostPreviewDto>>> GetAll();
        Task<Result<BlogPostDto>> GetWithDetails(long blogpostId);
        Task<Result<IEnumerable<BlogPostDto>>> Filter(BlogPostFilterDto blogPostFilterDto);
        #endregion

        #region Admin
        Task<Result<bool>> Add(BlogPostDto blogPostDto);
        Task<Result<bool>> Update(BlogPostDto blogPostDto);
        Task<Result<bool>> Delete(long id);
        #endregion
    }
}
