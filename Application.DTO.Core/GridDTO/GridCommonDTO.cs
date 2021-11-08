using Domain.Core.ModelFilter;

using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Core.GridDTO
{
    public class GridCommonDTO<TEntity> where TEntity : class
    {
        public int totalRegister { get; set; }
        public IEnumerable<TEntity> listResult { get; set; }
        public int totalPages { get; set; }
        public bool next { get; set; }
        public bool previous { get; set; }

        public GridCommonDTO<TEntity> CreateGridCommonDTO(IEnumerable<TEntity> lstTEntity, int total, BaseFilter oBaseFilter)
        {
            this.listResult = lstTEntity;
            this.totalRegister = total;
            this.totalPages = oBaseFilter.take > 0 ? Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(total) / oBaseFilter.take)) : 0;
            this.next = this.totalPages == 0 ? false : oBaseFilter.skip != this.totalPages - 1;
            this.previous = oBaseFilter.skip != 0;

            return this;
        }
    }
}
