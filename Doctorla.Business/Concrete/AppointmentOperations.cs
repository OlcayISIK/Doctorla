using Doctorla.Business.Abstract;
using Doctorla.Business.Helpers;
using Doctorla.Core.Communication;
using Doctorla.Core.Enums;
using Doctorla.Core.InternalDtos;
using Doctorla.Core.Utils;
using Doctorla.Data.Shared;
using Doctorla.Dto;
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
    public class AppointmentOperations : IAppointmentOperations
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CustomMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppSettings _appSettings;

        public AppointmentOperations(IUnitOfWork unitOfWork, CustomMapper mapper, IHttpContextAccessor httpContextAccessor, AppSettings appSettings)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appSettings;
        }

        #region User
        public async Task<Result<IEnumerable<AppointmentDto>>> GetAllForUser()
        {
            var claims = ClaimUtils.GetClaims(_httpContextAccessor.HttpContext.User.Claims);
            var appointments = _unitOfWork.Appointments.GetAll().Where(x => x.UserId == claims.Id);
            var dtos = await _mapper.ProjectTo<AppointmentDto>(appointments).ToListAsync();
            return Result<IEnumerable<AppointmentDto>>.CreateSuccessResult(dtos);
        }

        public async Task<Result<bool>> AddForUser(AppointmentDto appointmentDto)
        {
            var claims = ClaimUtils.GetClaims(_httpContextAccessor.HttpContext.User.Claims);
            var appointment = _mapper.Map<Appointment>(appointmentDto);
            appointment.AppointmentStatus = AppointmentStatus.Created;
            appointment.UserId = claims.Id;

            _unitOfWork.Appointments.Add(appointment);
            await _unitOfWork.Commit();

            return Result<bool>.CreateSuccessResult(true);
        }

        public async Task<Result<bool>> CancelAppointment(long appointmentId)
        {
            var claims = ClaimUtils.GetClaims(_httpContextAccessor.HttpContext.User.Claims);
            var appointment = await _unitOfWork.Appointments.GetAsTracking(appointmentId).Where(x => x.UserId == claims.Id).FirstOrDefaultAsync();
            appointment.AppointmentStatus = AppointmentStatus.CanceledByUser;
            await _unitOfWork.Commit();
            return Result<bool>.CreateSuccessResult(true);
        }
        #endregion

        #region Doctor
        public async Task<Result<IEnumerable<AppointmentDto>>> GetAllForDoctor()
        {
            var claims = ClaimUtils.GetClaims(_httpContextAccessor.HttpContext.User.Claims);
            var appointments = _unitOfWork.Appointments.GetAll().Where(x => x.DoctorId == claims.Id);
            var dtos = await _mapper.ProjectTo<AppointmentDto>(appointments).ToListAsync();
            return Result<IEnumerable<AppointmentDto>>.CreateSuccessResult(dtos);
        }

        public async Task<Result<bool>> ApproveAppointment(long appointmentId)
        {
            var claims = ClaimUtils.GetClaims(_httpContextAccessor.HttpContext.User.Claims);
            var appointment = await _unitOfWork.Appointments.GetAsTracking(appointmentId).Where(x => x.DoctorId == claims.Id).FirstOrDefaultAsync();
            appointment.AppointmentStatus = AppointmentStatus.Approved;
            //appointment.MeetingLink = MeetingCreator.CreateMeeting(_appSettings.ZoomApi);
            await _unitOfWork.Commit();
            return Result<bool>.CreateSuccessResult(true);
        }

        public async Task<Result<bool>> RejectAppointment(long appointmentId)
        {
            var claims = ClaimUtils.GetClaims(_httpContextAccessor.HttpContext.User.Claims);
            var appointment = await _unitOfWork.Appointments.GetAsTracking(appointmentId).Where(x => x.DoctorId == claims.Id).FirstOrDefaultAsync();
            appointment.AppointmentStatus = AppointmentStatus.CanceledByDoctor;
            await _unitOfWork.Commit();
            return Result<bool>.CreateSuccessResult(true);
        }
        #endregion
    }
}
