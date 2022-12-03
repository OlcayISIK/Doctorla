using AutoMapper;
using Doctorla.Core.Enums;
using Doctorla.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Business.Helpers
{
    public class CustomMapper
    {
        private readonly IMapper _mapper;

        public CustomMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        // TODO add an overload for language-less conversions
        /// <summary>
        /// Entity to DTO mapping
        /// </summary>
        public IQueryable<T> ProjectTo<T>(IQueryable query, Language language)
        {
            return _mapper.ProjectTo<T>(query, new { language });
        }

        public IQueryable<T> ProjectTo<T>(IQueryable query)
        {
            return _mapper.ProjectTo<T>(query);
        }

        /// <summary>
        /// Do not use this method unless you are absolutely sure!!
        /// </summary>
        public TDestination MapEntityToDto<TSource, TDestination>(TSource entity, Language language) where TSource : Entity
        {
            return _mapper.Map<TSource, TDestination>(entity, opts => opts.Items[nameof(Language)] = language);
        }

        /// <summary>
        /// Dto to Entity mapping
        /// </summary>
        public TEntity Map<TEntity>(object dto, Language language)
        {
            return _mapper.Map<TEntity>(dto, opts => opts.Items[nameof(Language)] = language);
        }
        public TEntity Map<TEntity>(object dto)
        {
            return _mapper.Map<TEntity>(dto);
        }

        /// <summary>
        /// In place mapping to be used in update operations. Updates only the given language properties.
        /// </summary>
        /// <param name="source">Source (DTO) object to be mapped</param>
        /// <param name="destination">Destination (Entity) object, which has other translations already filled.</param>
        /// <param name="language">The language currently being mapped.</param>
        public void Map<TSource, TDestination>(TSource source, TDestination destination, Language language = Language.Turkish) where TDestination : Entity
        {
            _mapper.Map(source, destination, opts => opts.Items[nameof(Language)] = language);
            //destination.LastModifiedAt = now;
        }
    }
}
