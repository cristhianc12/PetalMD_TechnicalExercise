using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Adapter.Core
{
	public static class AdapterProjection
	{
		private static IAdapter _adapter;

		public static void Initialize(IAdapter adapter)
		{
			_adapter = adapter;
		}

		public static TEntityDTO ProjectTo<TEntity, TEntityDTO>(this TEntity source)
		{
			return _adapter.Adapt<TEntity, TEntityDTO>(source);
		}

		public static IEnumerable<TEntityDTO> ProjectToCollection<TEntityDTO>(this IEnumerable<object> source)
		{
			return _adapter.Adapt<object, TEntityDTO>(source);
		}


	}
}
