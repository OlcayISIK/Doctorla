using AutoMapper;
using Doctorla.Core.Enums;
using Doctorla.Data;
using Doctorla.Data.Shared;
using Doctorla.Data.Shared.Blog;
using Doctorla.Dto;
using Doctorla.Dto.Shared;
using Doctorla.Dto.Shared.Blog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Collections.Specialized.BitVector32;

namespace Doctorla.Business.Helpers
{
    public class AutoMapperMappingProfile : Profile
    {
        public AutoMapperMappingProfile()
    {
        CreateMap(typeof(BatchDto<>), typeof(BatchDto<>));

        // automapper needs this variable for this configuration, although it does not use its value at runtime
        Language language = Language.English;
        CreateMap<MultiString, string>()
            .ConvertUsing(r => r == null ? null : language == Language.German ? r.German : language == Language.Turkish ? r.Turkish : (language == Language.English ? r.English : r.Spanish));
        CreateMap<string, MultiString>()
            .ForMember(dest => dest.English, opt => opt.MapFrom((src, dst, _, context) =>
            {
                Enum.TryParse(context.Options.Items["Language"].ToString(), out language);
                return MapLanguageBoundString(src, dst, Language.English, language);
            }))
            .ForMember(dest => dest.German, opt => opt.MapFrom((src, dst, _, context) =>
            {
                Enum.TryParse(context.Options.Items["Language"].ToString(), out language);
                return MapLanguageBoundString(src, dst, Language.German, language);
            }))
            .ForMember(dest => dest.Turkish, opt => opt.MapFrom((src, dst, _, context) =>
            {
                Enum.TryParse(context.Options.Items["Language"].ToString(), out language);
                return MapLanguageBoundString(src, dst, Language.Turkish, language);
            }))
            .ForMember(dest => dest.Spanish, opt => opt.MapFrom((src, dst, _, context) =>
            {
                Enum.TryParse(context.Options.Items["Language"].ToString(), out language);
                return MapLanguageBoundString(src, dst, Language.Spanish, language);
            }));

            CreateMap<Appointment, AppointmentDto>().ReverseMap();
            CreateMap<Specialty, SpecialtyDto>().ReverseMap();
            CreateMap<BlogPost, BlogPostDto>().ReverseMap();
    }

    private static string MapLanguageBoundString(string source, MultiString destination, Language destinationLanguage, Language sourceLanguage)
    {
        if (sourceLanguage == destinationLanguage)
            return source;

        return destinationLanguage switch
        {
            Language.English => destination.English,
            Language.German => destination.German,
            Language.Turkish => destination.Turkish,
            Language.Spanish => destination.Spanish,
            _ => throw new Exception("Language mapping is missing")
        };
    }
}
}
