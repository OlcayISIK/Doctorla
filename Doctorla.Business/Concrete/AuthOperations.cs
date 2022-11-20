using Doctorla.Business.Abstract;
using Doctorla.Business.Helpers;
using Doctorla.Business.Helpers.Token;
using Doctorla.Core;
using Doctorla.Core.Communication;
using Doctorla.Core.Enums;
using Doctorla.Core.InternalDtos;
using Doctorla.Core.Utils;
using Doctorla.Data;
using Doctorla.Dto;
using Doctorla.Dto.Auth;
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
    public class AuthOperations : IAuthOperations
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthOperations(IUnitOfWork unitOfWork, AppSettings appSettings, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings;
            _httpContextAccessor = httpContextAccessor;
        }

        #region Admin

        public async Task<Result<TokenDto>> AdminAuthenticateViaPassword(LoginDto loginDto)
        {
            //if (!Validate.Username(loginDto.Username) || !Validate.Password(loginDto.Password))
            //    return Result<TokenDto>.CreateErrorResult(ErrorCode.InvalidUsernameOrPassword);

            var admin = await _unitOfWork.Admins.Where(x => x.Email == loginDto.Email).FirstOrDefaultAsync();
            if (admin == null)
                return Result<TokenDto>.CreateErrorResult(ErrorCode.InvalidEmailOrPassword);

            var success = new CustomPasswordHasher().VerifyPassword(admin.HashedPassword, loginDto.Password);
            if (!success)
                return Result<TokenDto>.CreateErrorResult(ErrorCode.InvalidEmailOrPassword);

            var token = TokenCreator.CreateToken(admin.Id, admin.Email, AccountType.Admin, _appSettings.TokenOptions);
            await _unitOfWork.RedisTokens.Set(new RedisToken { TokenValue = token.RefreshToken, AccountId = admin.Id, AccountType = AccountType.Admin, TokenType = RedisTokenType.RefreshToken, Email = admin.Email }, _appSettings.TokenOptions.RefreshTokenLifetime);
            return Result<TokenDto>.CreateSuccessResult(token);
        }

        public async Task<Result<TokenDto>> AdminAuthenticateViaToken(string refreshToken)
        {
            var token = await _unitOfWork.RedisTokens.Get(refreshToken);
            if (token == null || token.AccountType != AccountType.Admin || token.TokenType != RedisTokenType.RefreshToken)
                return Result<TokenDto>.CreateErrorResult(ErrorCode.InvalidRefreshToken);
            await _unitOfWork.RedisTokens.Remove(refreshToken);

            var newToken = TokenCreator.CreateToken(token.AccountId, token.Email, AccountType.Admin, _appSettings.TokenOptions);
            await _unitOfWork.RedisTokens.Set(new RedisToken { TokenValue = newToken.RefreshToken, AccountId = token.AccountId }, _appSettings.TokenOptions.RefreshTokenLifetime);
            return Result<TokenDto>.CreateSuccessResult(newToken);
        }

        public async Task<Result<bool>> AdminLogout(string refreshToken)
        {
            await _unitOfWork.RedisTokens.Remove(refreshToken);
            return Result<bool>.CreateSuccessResult(true);
        }

        #endregion

        #region User

        public async Task<Result<TokenDto>> UserAuthenticateViaPassword(LoginDto loginDto)
        {
            //if (!Validate.Username(loginDto.Username) || !Validate.Password(loginDto.Password))
            //    return Result<TokenDto>.CreateErrorResult(ErrorCode.InvalidEmailOrPassword);
            var user = await _unitOfWork.Users.Where(x => x.Email == loginDto.Email).FirstOrDefaultAsync();
            if (user == default)
                return Result<TokenDto>.CreateErrorResult(ErrorCode.InvalidEmailOrPassword);         
            var success = new CustomPasswordHasher().VerifyPassword(user.HashedPassword, loginDto.Password);
            if (!success)
                return Result<TokenDto>.CreateErrorResult(ErrorCode.InvalidEmailOrPassword);     
            var token = TokenCreator.CreateToken(user.Id, user.Email, AccountType.User, _appSettings.TokenOptions);
            await _unitOfWork.RedisTokens.Set(new RedisToken { TokenValue = token.RefreshToken, AccountId = user.Id, AccountType = AccountType.User, TokenType = RedisTokenType.RefreshToken, Email = user.Email }, _appSettings.TokenOptions.RefreshTokenLifetime);
            return Result<TokenDto>.CreateSuccessResult(token);
        }

        public async Task<Result<TokenDto>> UserAuthenticateViaToken(string refreshToken)
        {
            var token = await _unitOfWork.RedisTokens.Get(refreshToken);
            if (token == null || token.AccountType != AccountType.User || token.TokenType != RedisTokenType.RefreshToken)
                return Result<TokenDto>.CreateErrorResult(ErrorCode.InvalidRefreshToken);
            await _unitOfWork.RedisTokens.Remove(refreshToken);
            var user = await _unitOfWork.Users.Get(token.AccountId).FirstOrDefaultAsync();
            var newToken = TokenCreator.CreateToken(user.Id, user.Email, AccountType.User, _appSettings.TokenOptions);
            await _unitOfWork.RedisTokens.Set(new RedisToken { TokenValue = newToken.RefreshToken, AccountId = token.AccountId, AccountType = AccountType.User, TokenType = RedisTokenType.RefreshToken, Email = user.Email }, _appSettings.TokenOptions.RefreshTokenLifetime);
            return Result<TokenDto>.CreateSuccessResult(newToken);
        }

        public async Task<Result<bool>> UserLogout(string refreshToken)
        {
            await _unitOfWork.RedisTokens.Remove(refreshToken);
            return Result<bool>.CreateSuccessResult(true);
        }

        public async Task<Result<bool>> UserForgotPassword(string emailAddress)
        {
            emailAddress = emailAddress.Trim().ToLower();
            var user = _unitOfWork.Users.Where(x => x.Email == emailAddress).FirstOrDefault();

            if (user == null)
                return Result<bool>.CreateErrorResult(ErrorCode.InvalidEmailOrPassword);

            var token = Generate.PasswordResetToken();
            // send confirmation mail
            var url = $"{ServiceLocator.AppSettings.ClientUrl}/userPasswordReset?token={token}";
            await _unitOfWork.RedisTokens.Set(new RedisToken
            {
                AccountId = user.Id,
                AccountType = AccountType.User,
                TokenType = RedisTokenType.PasswordResetToken,
                TokenValue = token
            }, 2 * 60);
            await EmailSender.Send(new Email
            {
                Subject = "Doctorla account password reset",
                Body = $"Here is the link for resetting your password : {url}",
                EmailToList = new List<string> { emailAddress }
            });
            await _unitOfWork.Commit();
            return Result<bool>.CreateSuccessResult(true);
        }

        public async Task<Result<bool>> UserResetPassword(ResetPasswordDto dto)
        {
            var token = await _unitOfWork.RedisTokens.Get(dto.PasswordResetToken);
            if (token == null || token.AccountType != AccountType.User || token.TokenType != RedisTokenType.PasswordResetToken)
                return Result<bool>.CreateErrorResult(ErrorCode.InvalidPasswordResetToken);
            var user = await _unitOfWork.Users.GetAsTracking(token.AccountId).FirstOrDefaultAsync();
            user.HashedPassword = new CustomPasswordHasher().HashPassword(dto.NewPassword);
            await _unitOfWork.Commit();
            await _unitOfWork.RedisTokens.Remove(dto.PasswordResetToken);
            return Result<bool>.CreateSuccessResult(true);
        }
        public async Task<Result<long>> UserSignUp(SignUpDto dto)
        {
            if (!Validate.Username(dto.Email) || !Validate.Password(dto.Password))
                return Result<long>.CreateErrorResult(ErrorCode.InvalidEmailOrPassword);
            var existingMail = await _unitOfWork.Users.Where(x => x.Email == dto.Email).FirstOrDefaultAsync();
            if (existingMail != null)
                return Result<long>.CreateErrorResult(ErrorCode.ObjectAlreadyExists);
            var now = DateTime.UtcNow;

            // create user
            var entity = _unitOfWork.Users.Add(new User
            {
                //CreatedAt = now,
                //Email = dto.Email,
                //LastModifiedAt = now,
                //Name = dto.Name,
                //Surname = dto.Surname,
                //PhoneNumber = dto.PhoneNumber,
                //HashedPassword = new CustomPasswordHasher().HashPassword(dto.Password),
                //// TODO fix what is wrong with user approving, then change this
                //Status = UserStatus.Created
            });
            await _unitOfWork.Commit();

            // TODO fix what is wrong with user approving, then change this
            // save approval token
            var approvalToken = Generate.ApprovalToken();
            await _unitOfWork.RedisTokens.Set(new RedisToken { AccountType = AccountType.User, TokenType = RedisTokenType.UserApprovalToken, AccountId = entity.Id, TokenValue = approvalToken }, 2 * 60);

            // TODO fix what is wrong with user approving, then change this
            // send confirmation mail
            var url = $"{ServiceLocator.AppSettings.ClientUrl}/userApproval?token={approvalToken}";
            await EmailSender.Send(new Email
            {
                Subject = "Doctorla Account Creation Confirmation",
                Body = $"Thank you for creating your account. In order to finish the process, please click on the following link : {url}",
                EmailToList = new List<string> { dto.Email }
            });
            return Result<long>.CreateSuccessResult(entity.Id);
        }
        public async Task<Result<bool>> DeleteUserAccount()
        {
            var claims = ClaimUtils.GetClaims(_httpContextAccessor.HttpContext.User.Claims);
            var userId = claims.Id;

            //_unitOfWork.Users.RemoveAccount(userId);
            await _unitOfWork.Commit();
            return Result<bool>.CreateSuccessResult(true);
        }

        #endregion

        #region Doctor

        public async Task<Result<TokenDto>> DoctorAuthenticateViaPassword(LoginDto loginDto)
        {
            if (!Validate.Username(loginDto.Username) || !Validate.Password(loginDto.Password))
                return Result<TokenDto>.CreateErrorResult(ErrorCode.InvalidEmailOrPassword);
            var doctor = await _unitOfWork.Doctors.Where(x => x.Name == loginDto.Username).FirstOrDefaultAsync();
            if (doctor == default)
                return Result<TokenDto>.CreateErrorResult(ErrorCode.InvalidEmailOrPassword);
            var success = new CustomPasswordHasher().VerifyPassword(doctor.Password, loginDto.Password);
            if (!success)
                return Result<TokenDto>.CreateErrorResult(ErrorCode.InvalidEmailOrPassword);
            var token = new TokenDto(); // TokenCreator.CreateDoctorToken(doctor.Id, doctor.Name, doctor.UserType, doctor.HospitalId, _appSettings.TokenOptions);
            await _unitOfWork.RedisTokens.Set(new RedisToken { TokenValue = token.RefreshToken, UserId = doctor.Id, AccountType = AccountType.Doctor, TokenType = RedisTokenType.RefreshToken, Username = doctor.Name }, _appSettings.TokenOptions.RefreshTokenLifetime);
            return Result<TokenDto>.CreateSuccessResult(token);
        }

        public async Task<Result<TokenDto>> DoctorAuthenticateViaToken(string refreshToken)
        {
            var token = await _unitOfWork.RedisTokens.Get(refreshToken);
            if (token == null || token.AccountType != AccountType.Doctor || token.TokenType != RedisTokenType.RefreshToken)
                return Result<TokenDto>.CreateErrorResult(ErrorCode.InvalidRefreshToken);
            await _unitOfWork.RedisTokens.Remove(refreshToken);
            var doctor = await _unitOfWork.Doctors.Get(token.UserId).FirstOrDefaultAsync();
            var newToken = new TokenDto();// TokenCreator.CreateDoctorToken(doctor.Id, doctor.Name, doctor.UserType, doctor.HospitalId, _appSettings.TokenOptions);
            await _unitOfWork.RedisTokens.Set(new RedisToken { TokenValue = newToken.RefreshToken, UserId = token.UserId, AccountType = AccountType.Doctor, TokenType = RedisTokenType.RefreshToken, Username = doctor.Name }, _appSettings.TokenOptions.RefreshTokenLifetime);
            return Result<TokenDto>.CreateSuccessResult(newToken);
        }

        public async Task<Result<bool>> DoctorLogout(string refreshToken)
        {
            await _unitOfWork.RedisTokens.Remove(refreshToken);
            return Result<bool>.CreateSuccessResult(true);
        }

        public async Task<Result<bool>> DoctorForgotPassword(string emailAddress)
        {
            emailAddress = emailAddress.Trim().ToLower();
            var doctor = _unitOfWork.Doctors.Where(x => x.Email == emailAddress).FirstOrDefault();

            if (doctor == null)
                return Result<bool>.CreateErrorResult(ErrorCode.InvalidEmailOrPassword);

            var token = Generate.PasswordResetToken();
            // send confirmation mail
            var url = $"{ServiceLocator.AppSettings.ClientUrl}/userPasswordReset?token={token}";
            await _unitOfWork.RedisTokens.Set(new RedisToken
            {
                UserId = doctor.Id,
                AccountType = AccountType.Doctor,
                TokenType = RedisTokenType.PasswordResetToken,
                TokenValue = token
            }, 2 * 60);
            await EmailSender.Send(new Email
            {
                Subject = "Doctorla account password reset",
                Body = $"Here is the link for resetting your password : {url}",
                EmailToList = new List<string> { emailAddress }
            });
            await _unitOfWork.Commit();
            return Result<bool>.CreateSuccessResult(true);
        }

        public async Task<Result<bool>> DoctorResetPassword(ResetPasswordDto dto)
        {
            var token = await _unitOfWork.RedisTokens.Get(dto.PasswordResetToken);
            if (token == null || token.AccountType != AccountType.Doctor || token.TokenType != RedisTokenType.PasswordResetToken)
                return Result<bool>.CreateErrorResult(ErrorCode.InvalidPasswordResetToken);
            var doctor = await _unitOfWork.Doctors.GetAsTracking(token.UserId).FirstOrDefaultAsync();
            doctor.Password = new CustomPasswordHasher().HashPassword(dto.NewPassword);
            await _unitOfWork.Commit();
            await _unitOfWork.RedisTokens.Remove(dto.PasswordResetToken);
            return Result<bool>.CreateSuccessResult(true);
        }

        public async Task<Result<long>> DoctorSignUp(SignUpDto dto)
        {
            if (!Validate.Username(dto.Email) || !Validate.Password(dto.Password))
                return Result<long>.CreateErrorResult(ErrorCode.InvalidEmailOrPassword);
            var existingMail = await _unitOfWork.Doctors.Where(x => x.Email == dto.Email).FirstOrDefaultAsync();
            if (existingMail != null)
                return Result<long>.CreateErrorResult(ErrorCode.ObjectAlreadyExists);
            var now = DateTime.UtcNow;

            // create user
            var entity = _unitOfWork.Doctors.Add(new Doctor
            {
                //CreatedAt = now,
                //Email = dto.Email,
                //LastModifiedAt = now,
                //Name = dto.Name,
                //Surname = dto.Surname,
                //HospitalId = dto.HospitalId,
                //PhoneNumber = dto.PhoneNumber,
                //HashedPassword = new CustomPasswordHasher().HashPassword(dto.Password),
                //// TODO fix what is wrong with user approving, then change this
                //Status = UserStatus.Created
            });
            await _unitOfWork.Commit();

            // TODO fix what is wrong with user approving, then change this
            // save approval token
            var approvalToken = Generate.ApprovalToken();
            await _unitOfWork.RedisTokens.Set(new RedisToken { AccountType = AccountType.Doctor, TokenType = RedisTokenType.UserApprovalToken, UserId = entity.Id, TokenValue = approvalToken }, 2 * 60);

            // TODO fix what is wrong with user approving, then change this
            // send confirmation mail
            var url = $"{ServiceLocator.AppSettings.ClientUrl}/userApproval?token={approvalToken}";
            await EmailSender.Send(new Email
            {
                Subject = "Doctorla Account Creation Confirmation",
                Body = $"Thank you for creating your account. In order to finish the process, please click on the following link : {url}",
                EmailToList = new List<string> { dto.Email }
            });
            return Result<long>.CreateSuccessResult(entity.Id);
        }

        public async Task<Result<bool>> ApproveDoctor(string approvalToken)
        {
            var token = await _unitOfWork.RedisTokens.Get(approvalToken);
            if (token == null || token.TokenType != RedisTokenType.UserApprovalToken || token.AccountType != AccountType.Doctor)
                return Result<bool>.CreateErrorResult(ErrorCode.ObjectNotFound);
            var doctor = await _unitOfWork.Doctors.GetAsTracking(token.UserId).FirstOrDefaultAsync();
            //doctor.Status = UserStatus.Approved;
            await _unitOfWork.Commit();
            await _unitOfWork.RedisTokens.Remove(approvalToken);
            return Result<bool>.CreateSuccessResult(true);
        }
        public async Task<Result<bool>> DeleteDoctorAccount()
        {
            var claims = ClaimUtils.GetClaims(_httpContextAccessor.HttpContext.User.Claims);
            var userId = claims.UserId;
            
            //_unitOfWork.Doctors.RemoveAccount(userId);
            await _unitOfWork.Commit();
            return Result<bool>.CreateSuccessResult(true);
        }

        #endregion

        #region Hospital
        public async Task<Result<TokenDto>> HospitalAuthenticateViaPassword(LoginDto loginDto)
        {
            if (!Validate.Username(loginDto.Username) || !Validate.Password(loginDto.Password))
                return Result<TokenDto>.CreateErrorResult(ErrorCode.InvalidEmailOrPassword);
            var hospital = new Hospital(); // await _unitOfWork.Hospitals.Where(x => x.Name == loginDto.Username).FirstOrDefaultAsync();
            if (hospital == default)
                return Result<TokenDto>.CreateErrorResult(ErrorCode.InvalidEmailOrPassword);
            var success = new CustomPasswordHasher().VerifyPassword(hospital.Password, loginDto.Password);
            if (!success)
                return Result<TokenDto>.CreateErrorResult(ErrorCode.InvalidEmailOrPassword);
            var token = TokenCreator.CreateHospitalToken(hospital.Id, hospital.Name, _appSettings.TokenOptions);
            await _unitOfWork.RedisTokens.Set(new RedisToken { TokenValue = token.RefreshToken, UserId = hospital.Id, AccountType = AccountType.Hospital, TokenType = RedisTokenType.RefreshToken, Username = hospital.Name }, _appSettings.TokenOptions.RefreshTokenLifetime);
            return Result<TokenDto>.CreateSuccessResult(token);
        }

        public async Task<Result<TokenDto>> HospitalAuthenticateViaToken(string refreshToken)
        {
            var token = await _unitOfWork.RedisTokens.Get(refreshToken);
            if (token == null || token.AccountType != AccountType.Hospital || token.TokenType != RedisTokenType.RefreshToken)
                return Result<TokenDto>.CreateErrorResult(ErrorCode.InvalidRefreshToken);
            await _unitOfWork.RedisTokens.Remove(refreshToken);
            var doctor = await _unitOfWork.Hospitals.Get(token.UserId).FirstOrDefaultAsync();
            var newToken = new TokenDto(); // TokenCreator.CreateHospitalToken(doctor.Id, doctor.Name, _appSettings.TokenOptions);
            await _unitOfWork.RedisTokens.Set(new RedisToken { TokenValue = newToken.RefreshToken, UserId = token.UserId, AccountType = AccountType.Hospital, TokenType = RedisTokenType.RefreshToken, Username = doctor.Name }, _appSettings.TokenOptions.RefreshTokenLifetime);
            return Result<TokenDto>.CreateSuccessResult(newToken);
        }

        public async Task<Result<bool>> HospitalLogout(string refreshToken)
        {
            await _unitOfWork.RedisTokens.Remove(refreshToken);
            return Result<bool>.CreateSuccessResult(true);
        }

        public async Task<Result<bool>> HospitalForgotPassword(string emailAddress)
        {
            emailAddress = emailAddress.Trim().ToLower();
            var hospital = _unitOfWork.Hospitals.Where(x => x.Email == emailAddress).FirstOrDefault();

            if (hospital == null)
                return Result<bool>.CreateErrorResult(ErrorCode.InvalidEmailOrPassword);

            var token = Generate.PasswordResetToken();
            // send confirmation mail
            var url = $"{ServiceLocator.AppSettings.ClientUrl}/userPasswordReset?token={token}";
            await _unitOfWork.RedisTokens.Set(new RedisToken
            {
                UserId = hospital.Id,
                AccountType = AccountType.Hospital,
                TokenType = RedisTokenType.PasswordResetToken,
                TokenValue = token
            }, 2 * 60);
            await EmailSender.Send(new Email
            {
                Subject = "Doctorla account password reset",
                Body = $"Here is the link for resetting your password : {url}",
                EmailToList = new List<string> { emailAddress }
            });
            await _unitOfWork.Commit();
            return Result<bool>.CreateSuccessResult(true);
        }

        public async Task<Result<bool>> HospitalResetPassword(ResetPasswordDto dto)
        {
            var token = await _unitOfWork.RedisTokens.Get(dto.PasswordResetToken);
            if (token == null || token.AccountType != AccountType.Hospital || token.TokenType != RedisTokenType.PasswordResetToken)
                return Result<bool>.CreateErrorResult(ErrorCode.InvalidPasswordResetToken);
            var doctor = await _unitOfWork.Hospitals.GetAsTracking(token.UserId).FirstOrDefaultAsync();
            //doctor.Password = new CustomPasswordHasher().HashPassword(dto.NewPassword);
            await _unitOfWork.Commit();
            await _unitOfWork.RedisTokens.Remove(dto.PasswordResetToken);
            return Result<bool>.CreateSuccessResult(true);
        }

        public async Task<Result<long>> HospitalSignUp(SignUpDto dto)
        {
            if (!Validate.Username(dto.Email) || !Validate.Password(dto.Password))
                return Result<long>.CreateErrorResult(ErrorCode.InvalidEmailOrPassword);
            //var existingMail = await _unitOfWork.Hospitals.Where(x => x.Email == dto.Email).FirstOrDefaultAsync();
            if (existingMail != null)
                return Result<long>.CreateErrorResult(ErrorCode.ObjectAlreadyExists);
            var now = DateTime.UtcNow;

            // create user
            var entity = _unitOfWork.Hospitals.Add(new Hospital
            {
                //CreatedAt = now,
                //Email = dto.Email,
                //LastModifiedAt = now,
                //Name = dto.Name,
                //PhoneNumber = dto.PhoneNumber,
                //HashedPassword = new CustomPasswordHasher().HashPassword(dto.Password),
                //// TODO fix what is wrong with user approving, then change this
                //Status = UserStatus.Created
            });
            await _unitOfWork.Commit();

            // TODO fix what is wrong with user approving, then change this
            // save approval token
            var approvalToken = Generate.ApprovalToken();
            await _unitOfWork.RedisTokens.Set(new RedisToken { AccountType = AccountType.Hospital, TokenType = RedisTokenType.UserApprovalToken, UserId = entity.Id, TokenValue = approvalToken }, 2 * 60);

            // TODO fix what is wrong with user approving, then change this
            // send confirmation mail
            var url = $"{ServiceLocator.AppSettings.ClientUrl}/userApproval?token={approvalToken}";
            await EmailSender.Send(new Email
            {
                Subject = "Doctorla Account Creation Confirmation",
                Body = $"Thank you for creating your account. In order to finish the process, please click on the following link : {url}",
                EmailToList = new List<string> { dto.Email }
            });
            return Result<long>.CreateSuccessResult(entity.Id);
        }

        public async Task<Result<bool>> ApproveHospital(string approvalToken)
        {
            var token = await _unitOfWork.RedisTokens.Get(approvalToken);
            if (token == null || token.TokenType != RedisTokenType.UserApprovalToken || token.AccountType != AccountType.Hospital)
                return Result<bool>.CreateErrorResult(ErrorCode.ObjectNotFound);
            var hospital = await _unitOfWork.Hospitals.GetAsTracking(token.UserId).FirstOrDefaultAsync();
            //hospital.Status = UserStatus.Approved;
            await _unitOfWork.Commit();
            await _unitOfWork.RedisTokens.Remove(approvalToken);
            return Result<bool>.CreateSuccessResult(true);
        }
        public async Task<Result<bool>> DeleteHospitalAccount()
        {
            var claims = ClaimUtils.GetClaims(_httpContextAccessor.HttpContext.User.Claims);
            var userId = claims.UserId;
            //_unitOfWork.Hospitals.RemoveAccount(userId);
            await _unitOfWork.Commit();
            return Result<bool>.CreateSuccessResult(true);
        }
        #endregion
    }
}
