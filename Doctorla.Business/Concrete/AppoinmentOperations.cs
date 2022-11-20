using Doctorla.Business.Abstract;
using Doctorla.Business.Helpers;
using Doctorla.Core.InternalDtos;
using Doctorla.Core.Utils;
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
    public class AppoinmentOperations : IAppointmentOperations
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CustomMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppoinmentOperations(IUnitOfWork unitOfWork, CustomMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        #region Doctor
        public async Task<Result<IEnumerable<AppointmentDto>>> GetAll()
        {
            var claims = ClaimUtils.GetClaims(_httpContextAccessor.HttpContext.User.Claims);
            var appointments = _unitOfWork.Appointments.GetAll().Where(x => x.DoctorId == claims.Id);
            var dtos = await _mapper.ProjectTo<AppointmentDto>(appointments).ToListAsync();
            return Result<IEnumerable<AppointmentDto>>.CreateSuccessResult(dtos);
        }

        public Task<Result<bool>> ApproveAppointment(long appointmentId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> RejectAppointment(long appointmentId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
