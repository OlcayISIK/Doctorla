using Doctorla.Business.Abstract;
using Doctorla.Business.Helpers;
using Doctorla.Core.Communication;
using Doctorla.Core.Enums;
using Doctorla.Core.InternalDtos;
using Doctorla.Core.Utils;
using Doctorla.Data.Shared;
using Doctorla.Dto;
using Doctorla.Dto.Payment;
using Doctorla.Dto.Shared;
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
    public class IyzicoOperations : IIyzicoOperations
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CustomMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppSettings _appSettings;

        public IyzicoOperations(IUnitOfWork unitOfWork, CustomMapper mapper, IHttpContextAccessor httpContextAccessor, AppSettings appSettings)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appSettings;
        }

        #region User
        public async Task<Result<bool>> PayForAppointment(PaymentDto paymentDto)
        {
            var claims = ClaimUtils.GetClaims(_httpContextAccessor.HttpContext.User.Claims);

            var appointments = _unitOfWork.Appointments.GetAll().Where(x => x.UserId == claims.Id);
            var dtos = await _mapper.ProjectTo<AppointmentDto>(appointments).ToListAsync();


            paymentDto.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            var user = await _unitOfWork.Users.Get(claims.Id).FirstOrDefaultAsync();
            var appointment = await _unitOfWork.Appointments.Get(paymentDto.AppointmentId).Where(x => x.UserId == claims.Id).FirstOrDefaultAsync();
            var paymentResult = IyzicoHelper.MakePayment(_appSettings.Iyzico, paymentDto, user, appointment);

            //Todo - add specialized enums for error status
            return paymentResult ? Result<bool>.CreateSuccessResult(true) : Result<bool>.CreateErrorResult(ErrorCode.InternalServerError);
        }
        #endregion
    }
}
