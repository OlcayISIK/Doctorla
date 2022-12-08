using Doctorla.Business.Abstract;
using Doctorla.Business.Helpers;
using Doctorla.Core.Enums;
using Doctorla.Core.InternalDtos;
using Doctorla.Core.Utils;
using Doctorla.Data.Entities.SystemAppoinments;
using Doctorla.Data.Members;
using Doctorla.Data.Shared.Blog;
using Doctorla.Dto;
using Doctorla.Dto.Members;
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
    public class DoctorOperations : IDoctorOperations
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CustomMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DoctorOperations(IUnitOfWork unitOfWork, CustomMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        #region Shared
        public async Task<Result<IEnumerable<DoctorPreviewDto>>> GetAll()
        {
            var doctors = _unitOfWork.Doctors.GetAll();
            var dtos = await _mapper.ProjectTo<DoctorPreviewDto>(doctors).ToListAsync();
            return Result<IEnumerable<DoctorPreviewDto>>.CreateSuccessResult(dtos);
        }

        public async Task<Result<DoctorDto>> GetWithDetails(long doctodId)
        {
            var doctor = _unitOfWork.Doctors.Get(doctodId);
            var dto = await _mapper.ProjectTo<DoctorDto>(doctor).FirstOrDefaultAsync();
            return Result<DoctorDto>.CreateSuccessResult(dto);
        }

        public async Task<Result<IEnumerable<DoctorPreviewDto>>> GetAllAvailableInGivenDate(DateTime date)
        {
            var doctors = _unitOfWork.Doctors.GetAllAvailableInGivenDate(date);
            var dto = await _mapper.ProjectTo<DoctorPreviewDto>(doctors).ToListAsync();
            return Result<IEnumerable<DoctorPreviewDto>>.CreateSuccessResult(dto);
        }
        #endregion

        #region Admin
        public async Task<Result<bool>> Add(DoctorDto doctorDto)
        {
            var entity = _mapper.Map<Doctor>(doctorDto);
            _unitOfWork.Doctors.Add(entity);
            await _unitOfWork.Commit();

            return Result<bool>.CreateSuccessResult(true);
        }

        public async Task<Result<bool>> Update(DoctorDto doctorDto)
        {
            var entity = await _unitOfWork.Specialties.GetAsTracking(doctorDto.Id).FirstOrDefaultAsync();
            _mapper.Map(doctorDto, entity);
            await _unitOfWork.Commit();

            return Result<bool>.CreateSuccessResult(true);
        }

        public async Task<Result<bool>> Delete(long id)
        {
            var doctor = await _unitOfWork.Doctors.GetAsTracking(id).FirstOrDefaultAsync();
            doctor.IsDeleted = true;
            await _unitOfWork.Commit();
            return Result<bool>.CreateSuccessResult(true);
        }
        #endregion
    }
}
