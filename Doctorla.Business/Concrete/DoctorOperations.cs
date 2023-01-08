using Doctorla.Business.Abstract;
using Doctorla.Business.Helpers;
using Doctorla.Core.Enums;
using Doctorla.Core.InternalDtos;
using Doctorla.Core.Utils;
using Doctorla.Data.Entities.SystemAppoinments;
using Doctorla.Data.Members.DoctorEntity;
using Doctorla.Data.Shared.Blog;
using Doctorla.Dto;
using Doctorla.Dto.Members;
using Doctorla.Dto.Members.DoctorEntity;
using Doctorla.Dto.Members.Profile;
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

        public async Task<Result<DoctorDto>> Get(long? doctodId = null)
        {
            var doctor = _unitOfWork.Doctors.Get(doctodId ?? ClaimUtils.GetClaims(_httpContextAccessor.HttpContext.User.Claims).Id);
            var dto = await _mapper.ProjectTo<DoctorDto>(doctor).FirstOrDefaultAsync();
            return Result<DoctorDto>.CreateSuccessResult(dto);
        }

        public async Task<Result<DoctorDetailsDto>> GetDoctorDetails(long? doctodId = null)
        {
            var doctorDetails = _unitOfWork.Doctors.GetDoctorDetails(doctodId ?? ClaimUtils.GetClaims(_httpContextAccessor.HttpContext.User.Claims).Id);

            var detailsDto = new DoctorDetailsDto();
            detailsDto.Educations = await _mapper.ProjectTo<DoctorEducationDto>(doctorDetails.DoctorEducations).ToListAsync();
            detailsDto.Experiences = await _mapper.ProjectTo<DoctorExperienceDto>(doctorDetails.DoctorExperiences).ToListAsync();
            detailsDto.MedicalInterests =  await _mapper.ProjectTo<DoctorMedicalInterestDto>(doctorDetails.DoctorMedicalInterests).ToListAsync();
            detailsDto.ScientificMemberships = await _mapper.ProjectTo<DoctorScientificMembershipDto>(doctorDetails.DoctorScientificMembership).ToListAsync();

            return Result<DoctorDetailsDto>.CreateSuccessResult(detailsDto);
        }

        //public async Task<Result<IEnumerable<DoctorPreviewDto>>> GetAllAvailableInGivenDate(DateTime date)
        //{
        //    var doctors = _unitOfWork.Doctors.GetAllAvailableInGivenDate(date);
        //    var dto = await _mapper.ProjectTo<DoctorPreviewDto>(doctors).ToListAsync();
        //    return Result<IEnumerable<DoctorPreviewDto>>.CreateSuccessResult(dto);
        //}
        #endregion

        #region Doctor
        public async Task<Result<bool>> UpdateForDoctor(DoctorDto doctorDto)
        {
            var claims = ClaimUtils.GetClaims(_httpContextAccessor.HttpContext.User.Claims);
            var entity = await _unitOfWork.Doctors.GetAsTracking(claims.Id).FirstOrDefaultAsync();
            _mapper.Map(doctorDto, entity);
            await _unitOfWork.Commit();

            return Result<bool>.CreateSuccessResult(true);
        }

        public async Task<Result<bool>> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            if (!Validate.Password(changePasswordDto.NewPassword))
                return Result<bool>.CreateErrorResult(ErrorCode.InvalidEmailOrPassword);
            var doctorId = ClaimUtils.GetClaims(_httpContextAccessor.HttpContext.User.Claims).Id;
            var entity = await _unitOfWork.Doctors.GetAsTracking(doctorId).FirstOrDefaultAsync();
            var cph = new CustomPasswordHasher();
            var success = cph.VerifyPassword(entity.HashedPassword, changePasswordDto.OldPassword);
            if (!success)
                return Result<bool>.CreateErrorResult(ErrorCode.InvalidEmailOrPassword);
            entity.HashedPassword = cph.HashPassword(changePasswordDto.NewPassword);
            await _unitOfWork.Commit();
            return Result<bool>.CreateSuccessResult(true);
        }
        #endregion

        #region Admin
        public async Task<Result<bool>> Add(DoctorDto doctorDto)
        {
            var entity = _mapper.Map<Doctor>(doctorDto);
            _unitOfWork.Doctors.Add(entity);
            await _unitOfWork.Commit();

            var educations = _mapper.Map<List<DoctorEducation>>(doctorDto.EducationsBatch?.AddedItems);
            var experiences = _mapper.Map<List<DoctorExperience>>(doctorDto.ExperienceBatch?.AddedItems);
            var medicalInterests = _mapper.Map<List<DoctorMedicalInterest>>(doctorDto.MedicalInterestBatch?.AddedItems);
            var scientificMemberships = _mapper.Map<List<DoctorScientificMembership>>(doctorDto.ScientificMembershipBatch?.AddedItems);

            educations.ForEach(x => { x.Doctor = entity; });
            experiences.ForEach(x => { x.Doctor = entity; });
            medicalInterests.ForEach(x => { x.Doctor = entity; });
            scientificMemberships.ForEach(x => { x.Doctor = entity; });

            _unitOfWork.DoctorEducations.AddRange(educations);
            _unitOfWork.DoctorExperiences.AddRange(experiences);
            _unitOfWork.DoctorMedicalInterests.AddRange(medicalInterests);
            _unitOfWork.DoctorScientificMemberships.AddRange(scientificMemberships);

            return Result<bool>.CreateSuccessResult(true);
        }

        public async Task<Result<bool>> UpdateForAdmin(DoctorDto doctorDto)
        {
            var entity = await _unitOfWork.Doctors.GetAsTracking(doctorDto.Id).FirstOrDefaultAsync();
            _mapper.Map(doctorDto, entity);
            await _unitOfWork.Commit();

            //Todo - refactor this
            ExtractDoctorDetailEntities<DoctorEducationDto, DoctorEducation>(doctorDto.EducationsBatch, out var educationsBatch);
            educationsBatch.AddedItems.ForEach(x => { x.Doctor = entity; });
            educationsBatch.UpdatedItems.ForEach(x => { x.Doctor = entity; });
            educationsBatch.RemovedItems.ForEach(x => { x.Doctor = entity; });

            ExtractDoctorDetailEntities<DoctorExperienceDto, DoctorExperience>(doctorDto.ExperienceBatch, out var experiencesBatch);
            experiencesBatch.AddedItems.ForEach(x => { x.Doctor = entity; });
            experiencesBatch.UpdatedItems.ForEach(x => { x.Doctor = entity; });
            experiencesBatch.RemovedItems.ForEach(x => { x.Doctor = entity; });

            ExtractDoctorDetailEntities<DoctorMedicalInterestDto, DoctorMedicalInterest>(doctorDto.MedicalInterestBatch, out var medicalInteretsBatch);
            medicalInteretsBatch.AddedItems.ForEach(x => { x.Doctor = entity; });
            medicalInteretsBatch.UpdatedItems.ForEach(x => { x.Doctor = entity; });
            medicalInteretsBatch.RemovedItems.ForEach(x => { x.Doctor = entity; });

            ExtractDoctorDetailEntities<DoctorScientificMembershipDto, DoctorScientificMembership>(doctorDto.ScientificMembershipBatch, out var scientificMembershipsBatch);
            scientificMembershipsBatch.AddedItems.ForEach(x => { x.Doctor = entity; });
            scientificMembershipsBatch.UpdatedItems.ForEach(x => { x.Doctor = entity; });
            scientificMembershipsBatch.RemovedItems.ForEach(x => { x.Doctor = entity; });

            _unitOfWork.DoctorEducations.BatchUpdate(educationsBatch);
            _unitOfWork.DoctorExperiences.BatchUpdate(experiencesBatch);
            _unitOfWork.DoctorMedicalInterests.BatchUpdate(medicalInteretsBatch);
            _unitOfWork.DoctorScientificMemberships.BatchUpdate(scientificMembershipsBatch);

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

        #region Helpers
        private void ExtractDoctorDetailEntities<TDto, TEntity>(BatchDto<TDto> dtos, out BatchDto<TEntity> entitiesBatch)
        {
            if (dtos == null)
            {
                entitiesBatch = null;
                return;
            }

            entitiesBatch = new BatchDto<TEntity>();

            foreach (var detailDto in dtos.AddedItems)
            {
                var entity = _mapper.Map<TEntity>(detailDto);
                entitiesBatch.AddedItems.Add(entity);
            }

            foreach (var detailDto in dtos.UpdatedItems)
            {
                var entity = _mapper.Map<TEntity>(detailDto);
                entitiesBatch.UpdatedItems.Add(entity);
            }

            foreach (var detailDto in dtos.RemovedItems)
            {
                var entity = _mapper.Map<TEntity>(detailDto);
                entitiesBatch.RemovedItems.Add(entity);
            }
        }
        #endregion
    }
}
