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

        public AppointmentOperations(IUnitOfWork unitOfWork, CustomMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        #region Shared
        public async Task<Result<bool>> CreateAppointment(AppointmentDto appointmentDto)
        {
            var claims = ClaimUtils.GetClaims(_httpContextAccessor.HttpContext.User.Claims);
            var appointment = _mapper.Map<Appointment>(appointmentDto);
            
            if(claims.AccountType == AccountType.Doctor)
            {
                appointment.OwnedByDoctor = true;
                appointment.DoctorId = claims.Id;
                appointment.AppointmentStatus = AppointmentStatus.Active;
            }
            else
            {
                appointment.OwnedByDoctor = false;
                appointment.UserId = claims.Id;
                appointment.DoctorId = appointment.DoctorId;
                appointment.AppointmentStatus = AppointmentStatus.Requested;
            }

            _unitOfWork.Appointments.Add(appointment);
            await _unitOfWork.Commit();

            return Result<bool>.CreateSuccessResult(true);
        }
        #endregion

        #region User
        public async Task<Result<IEnumerable<AppointmentDto>>> GetAllForUser()
        {
            var claims = ClaimUtils.GetClaims(_httpContextAccessor.HttpContext.User.Claims);
            var appointments = _unitOfWork.Appointments.GetAll().Where(x => x.UserId == claims.Id);
            var dtos = await _mapper.ProjectTo<AppointmentDto>(appointments).ToListAsync();
            return Result<IEnumerable<AppointmentDto>>.CreateSuccessResult(dtos);
        }

        public async Task<Result<IEnumerable<AppointmentDto>>> GetAvailableAppointments(long doctorId)
        {
            var claims = ClaimUtils.GetClaims(_httpContextAccessor.HttpContext.User.Claims);
            var appointments = _unitOfWork.Appointments.GetAll().Where(x => x.DoctorId == doctorId && x.AppointmentStatus == AppointmentStatus.Active);
            var dtos = await _mapper.ProjectTo<AppointmentDto>(appointments).ToListAsync();
            return Result<IEnumerable<AppointmentDto>>.CreateSuccessResult(dtos);
        }

        public async Task<Result<bool>> RequestAppointment(long appointmentId)
        {
            var claims = ClaimUtils.GetClaims(_httpContextAccessor.HttpContext.User.Claims);
            var appointment = await _unitOfWork.Appointments.Where(x=>x.Id == appointmentId && x.AppointmentStatus == AppointmentStatus.Active && x.OwnedByDoctor).AsTracking().FirstOrDefaultAsync();
            appointment.AppointmentStatus = AppointmentStatus.Approved;
            appointment.UserId = claims.Id;

            await _unitOfWork.Commit();
            return Result<bool>.CreateSuccessResult(true);
        }

        public async Task<Result<bool>> CancelAppointmentForUser(long appointmentId)
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
            var appointment = await _unitOfWork.Appointments.GetAsTracking(appointmentId).Where(x => x.DoctorId == claims.Id && !x.OwnedByDoctor).FirstOrDefaultAsync();
            appointment.AppointmentStatus = AppointmentStatus.Approved;
            appointment.MeetingLink = Guid.NewGuid().ToString();//MeetingCreator.CreateMeeting(_appSettings.ZoomApi);
            await _unitOfWork.Commit();
            return Result<bool>.CreateSuccessResult(true);
        }

        public async Task<Result<bool>> CancelAppointmentForDoctor(long appointmentId)
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
