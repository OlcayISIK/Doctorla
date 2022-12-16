using Doctorla.Dto;
using Doctorla.Dto.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Business.Abstract
{
    public interface IAuthOperations
    {
        #region Admin

        Task<Result<TokenDto>> AdminAuthenticateViaPassword(LoginDto loginDto);
        Task<Result<TokenDto>> AdminAuthenticateViaToken(string refreshToken);
        Task<Result<bool>> AdminLogout(string refreshToken);

        #endregion

        #region User

        Task<Result<TokenDto>> UserAuthenticateViaPassword(LoginDto loginDto);
        Task<Result<TokenDto>> UserAuthenticateViaToken(string refreshToken);
        Task<Result<bool>> UserLogout(string refreshToken);
        Task<Result<bool>> UserForgotPassword(string emailAddress);
        Task<Result<bool>> UserResetPassword(ResetPasswordDto dto);
        Task<Result<long>> UserSignUp(UserSignUpDto dto);
        Task<Result<bool>> DeleteUserAccount();



        #endregion

        #region Doctor

        Task<Result<TokenDto>> DoctorAuthenticateViaPassword(LoginDto loginDto);
        Task<Result<TokenDto>> DoctorAuthenticateViaToken(string refreshToken);
        Task<Result<bool>> DoctorLogout(string refreshToken);
        Task<Result<bool>> DoctorForgotPassword(string emailAddress);
        Task<Result<bool>> DoctorResetPassword(ResetPasswordDto dto);
        Task<Result<long>> DoctorSignUp(DoctorSignUpDto dto);
        Task<Result<bool>> ApproveDoctor(string approvalToken);
        Task<Result<bool>> DeleteDoctorAccount();

        #endregion

        #region Hospital

        Task<Result<TokenDto>> HospitalAuthenticateViaPassword(LoginDto loginDto);
        Task<Result<TokenDto>> HospitalAuthenticateViaToken(string refreshToken);
        Task<Result<bool>> HospitalLogout(string refreshToken);
        Task<Result<bool>> HospitalForgotPassword(string emailAddress);
        Task<Result<bool>> HospitalResetPassword(ResetPasswordDto dto);
        Task<Result<long>> HospitalSignUp(UserSignUpDto dto);
        Task<Result<bool>> ApproveHospital(string approvalToken);
        Task<Result<bool>> DeleteHospitalAccount();

        #endregion



    }
}
