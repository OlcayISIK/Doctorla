using Doctorla.Business.Abstract;
using Doctorla.Business.Helpers;
using Doctorla.Core.Enums;
using Doctorla.Core.InternalDtos;
using Doctorla.Core.Utils;
using Doctorla.Data.Entities.SystemAppoinments;
using Doctorla.Data.Shared.Blog;
using Doctorla.Dto;
using Doctorla.Dto.Shared.Blog;
using Doctorla.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Business.Concrete
{
    public class BlogPostOperations : IBlogPostOperations
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CustomMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BlogPostOperations(IUnitOfWork unitOfWork, CustomMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        #region Shared
        public async Task<Result<IEnumerable<BlogPostPreviewDto>>> GetAll()
        {
            var blogPosts = _unitOfWork.BlogPosts.GetAll();
            var dtos = await _mapper.ProjectTo<BlogPostPreviewDto>(blogPosts).ToListAsync();
            return Result<IEnumerable<BlogPostPreviewDto>>.CreateSuccessResult(dtos);
        }

        public async Task<Result<BlogPostDto>> GetWithDetails(long blogpostId)
        {
            var blogPost = _unitOfWork.BlogPosts.Get(blogpostId);
            var dto = await _mapper.ProjectTo<BlogPostDto>(blogPost).FirstOrDefaultAsync();
            return Result<BlogPostDto>.CreateSuccessResult(dto);
        }

        public async Task<Result<IEnumerable<BlogPostDto>>> Filter(BlogPostFilterDto blogPostFilterDto)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Admin
        public async Task<Result<bool>> Add(BlogPostDto blogPostDto)
        {
            var specialty = _mapper.Map<BlogPost>(blogPostDto);
            _unitOfWork.BlogPosts.Add(specialty);
            await _unitOfWork.Commit();

            return Result<bool>.CreateSuccessResult(true);
        }

        public async Task<Result<bool>> Update(BlogPostDto blogPostDto)
        {
            var entity = await _unitOfWork.Specialties.GetAsTracking(blogPostDto.Id).FirstOrDefaultAsync();
            _mapper.Map(blogPostDto, entity);
            await _unitOfWork.Commit();

            return Result<bool>.CreateSuccessResult(true);
        }

        public async Task<Result<bool>> Delete(long id)
        {
            var specialty = await _unitOfWork.BlogPosts.GetAsTracking(id).FirstOrDefaultAsync();
            specialty.IsDeleted = true;
            await _unitOfWork.Commit();
            return Result<bool>.CreateSuccessResult(true);
        }
        #endregion
    }
}
