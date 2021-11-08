using AutoMapper;

using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Adapter.Core
{
	public class AdapterAutoMapper : IAdapter
	{
		private readonly IMapper _mapper;

		public AdapterAutoMapper(Profile[] profiles)
		{
			var config = new MapperConfiguration(x => x.AddProfiles(profiles));
			_mapper = new Mapper(config);
			AdapterProjection.Initialize(this);
		}

		public TEntityDTO Adapt<TEntity, TEntityDTO>(TEntity source)
		{
			var adapte = _mapper.Map<TEntity, TEntityDTO>(source);
			return adapte;
		}

		public IEnumerable<TEntityDTO> Adapt<TEntity, TEntityDTO>(IEnumerable<TEntity> source)
		{
			var adapte = _mapper.Map<IEnumerable<TEntity>, IEnumerable<TEntityDTO>>(source);
			return adapte;
		}
	}
}
