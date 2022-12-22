using Doctorla.Business.Abstract;
using Doctorla.Business.Helpers;
using Doctorla.Core.Enums;
using Doctorla.Core.Utils;
using Doctorla.Data.Members;
using Doctorla.Data.Members.DoctorEntity;
using Doctorla.Dto;
using Doctorla.Dto.Members;
using Doctorla.Dto.Members.DoctorEntity;
using Doctorla.Dto.Members.Profile;
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
    public class UserOperations : IUserOperations
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CustomMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserOperations(IUnitOfWork unitOfWork, CustomMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<bool>> Add(UserDto userDto)
        {
            var entity = _mapper.Map<User>(userDto);
            _unitOfWork.Users.Add(entity);
            await _unitOfWork.Commit();
            return Result<bool>.CreateSuccessResult(true);

        }

        public async Task<Result<bool>> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            if (!Validate.Password(changePasswordDto.NewPassword))
                return Result<bool>.CreateErrorResult(ErrorCode.InvalidEmailOrPassword);
            var userId = ClaimUtils.GetClaims(_httpContextAccessor.HttpContext.User.Claims).Id;
            var entity = await _unitOfWork.Users.GetAsTracking(userId).FirstOrDefaultAsync();
            var cph = new CustomPasswordHasher();
            var success = cph.VerifyPassword(entity.HashedPassword, changePasswordDto.OldPassword);
            if (!success)
                return Result<bool>.CreateErrorResult(ErrorCode.InvalidEmailOrPassword);
            entity.HashedPassword = cph.HashPassword(changePasswordDto.NewPassword);
            await _unitOfWork.Commit();
            return Result<bool>.CreateSuccessResult(true);
        }

        public async Task<Result<bool>> Delete(long id)
        {
            var user = await _unitOfWork.Users.GetAsTracking(id).FirstOrDefaultAsync();
            user.IsDeleted = true;
            await _unitOfWork.Commit();
            return Result<bool>.CreateSuccessResult(true);
        }

        public async Task<Result<UserDto>> Get(long? userId = null)
        {
            var user = _unitOfWork.Users.Get(userId ?? ClaimUtils.GetClaims(_httpContextAccessor.HttpContext.User.Claims).Id);
            var dto = await _mapper.ProjectTo<UserDto>(user).FirstOrDefaultAsync();
            return Result<UserDto>.CreateSuccessResult(dto);
        }

        public async Task<Result<IEnumerable<UserDto>>> GetAll()
        {
            var users = _unitOfWork.Users.GetAll();
            var dtos = await _mapper.ProjectTo<UserDto>(users).ToListAsync();
            return Result<IEnumerable<UserDto>>.CreateSuccessResult(dtos);
        }

        public async Task<Result<bool>> UpdateForAdmin(UserDto userDto)
        {
            var entity = await _unitOfWork.Users.GetAsTracking(userDto.Id).FirstOrDefaultAsync();
            _mapper.Map(userDto, entity);
            await _unitOfWork.Commit();
            return Result<bool>.CreateSuccessResult(true);
        }

        public async Task<Result<bool>> UpdateForUser(UserDto userDto)
        {
            var claims = ClaimUtils.GetClaims(_httpContextAccessor.HttpContext.User.Claims);
            var entity = await _unitOfWork.Doctors.GetAsTracking(claims.Id).FirstOrDefaultAsync();
            _mapper.Map(userDto, entity);
            await _unitOfWork.Commit();

            return Result<bool>.CreateSuccessResult(true);
        }
    }
}
