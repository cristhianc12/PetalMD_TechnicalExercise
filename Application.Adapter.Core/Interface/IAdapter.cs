using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Adapter.Core
{
    public interface IAdapter
    {
        TEntityDTO Adapt<TEntity, TEntityDTO>(TEntity source);
        IEnumerable<TEntityDTO> Adapt<TEntity, TEntityDTO>(IEnumerable<TEntity> source);
    }
}
