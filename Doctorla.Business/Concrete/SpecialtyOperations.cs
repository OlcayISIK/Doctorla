using Doctorla.Business.Abstract;
using Doctorla.Business.Helpers;
using Doctorla.Core.Enums;
using Doctorla.Core.InternalDtos;
using Doctorla.Core.Utils;
using Doctorla.Data.Entities.SystemAppoinments;
using Doctorla.Data.Shared;
using Doctorla.Dto;
using Doctorla.Dto.Members;
using Doctorla.Dto.Members.DoctorEntity;
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
    public class SpecialtyOperations : ISpecialtyOperations
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CustomMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SpecialtyOperations(IUnitOfWork unitOfWork, CustomMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        #region Shared
        public async Task<Result<IEnumerable<SpecialtyDto>>> GetAll()
        {
            var specialties = _unitOfWork.Specialties.GetAll();
            var dtos = await _mapper.ProjectTo<SpecialtyDto>(specialties).ToListAsync();
            return Result<IEnumerable<SpecialtyDto>>.CreateSuccessResult(dtos);
        }

        public async Task<Result<SpecialtyDto>> Get(long id)
        {
            var specialties = _unitOfWork.Specialties.Get(id);
            var dto = await _mapper.ProjectTo<SpecialtyDto>(specialties).FirstOrDefaultAsync();
            return Result<SpecialtyDto>.CreateSuccessResult(dto);
        }

        public async Task<Result<IEnumerable<DoctorDto>>> GetDoctorWithSpecialities(long id)
        {
            var doctorWithSpecialities = _unitOfWork.Doctors.GetDoctorWithSpecialities(id);
            var dtos = await _mapper.ProjectTo<DoctorDto>(doctorWithSpecialities).ToListAsync();
            return Result < IEnumerable<DoctorDto>>.CreateSuccessResult(dtos);
        }
        #endregion

        #region Admin
        public async Task<Result<bool>> Add(SpecialtyDto specialtyDto)
        {
            var specialty = _mapper.Map<Specialty>(specialtyDto);
            _unitOfWork.Specialties.Add(specialty);
            await _unitOfWork.Commit();

            return Result<bool>.CreateSuccessResult(true);
        }

        public async Task<Result<bool>> Update(SpecialtyDto specialtyDto)
        {
            var entity = await _unitOfWork.Specialties.GetAsTracking(specialtyDto.Id).FirstOrDefaultAsync();
            _mapper.Map(specialtyDto, entity);
            await _unitOfWork.Commit();

            return Result<bool>.CreateSuccessResult(true);
        }

        public async Task<Result<bool>> Delete(long id)
        {
            var specialty = await _unitOfWork.Specialties.GetAsTracking(id).FirstOrDefaultAsync();
            specialty.IsDeleted = true;
            await _unitOfWork.Commit();
            return Result<bool>.CreateSuccessResult(true);
        }
        #endregion
    }
}
