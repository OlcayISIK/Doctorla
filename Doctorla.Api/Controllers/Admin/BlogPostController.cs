using Doctorla.Business.Abstract;
using Doctorla.Core;
using Doctorla.Data.Shared;
using Doctorla.Dto;
using Doctorla.Dto.Auth;
using Doctorla.Dto.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doctorla.Api.Controllers.Admin
{
    /// <summary>
    /// Authentication related endpoints
    /// </summary>
    [ApiController]
    [Route("api/admin/[controller]")]
    [ApiExplorerSettings(GroupName = Constants.AuthenticationSchemes.Admin)]
    public class BlogPostController
    {
        private readonly IBlogPostOperations _operations;

        /// <inheritdoc />
        public BlogPostController(IBlogPostOperations operations)
        {
            _operations = operations;
        }

        [HttpGet("getall")]
        public async Task<Result<IEnumerable<BlogPostDto>>> GetAll()
        {
            return await _operations.GetAll();
        }

        [HttpPost("add")]
        public async Task<Result<bool>> Add(BlogPostDto blogPostDto)
        {
            return await _operations.Add(blogPostDto);
        }

        [HttpPut("update")]
        public async Task<Result<bool>> Update(BlogPostDto blogPostDto)
        {
            return await _operations.Update(blogPostDto);
        }

        [HttpDelete("delete")]
        public async Task<Result<bool>> Delete(long id)
        {
            return await _operations.Delete(id);
        }
    }
}
