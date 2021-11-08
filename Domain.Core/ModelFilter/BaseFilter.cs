using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Core.ModelFilter
{
    public class BaseFilter
    {
        /// <summary>
        /// Texto de búsqueda
        /// </summary>
        public string searchText { get; set; }
        /// <summary>
        /// Número de página
        /// </summary>
        public int skip { get; set; }
        /// <summary>
        /// Número de registro por página
        /// </summary>
        public int take { get; set; }
        /// <summary>
        /// Id de búsqueda
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// Orden true: asc - false: desc
        /// </summary>
        public bool desc { get; set; }
        /// <summary>
        /// Columna por la que se hace el ordenamiento
        /// </summary>
        public string columnOrderBy { get; set; }
        /// <summary>
        /// filtro de estado
        /// </summary>
        public FilterActiveEntity state { get; set; }
    }
}
