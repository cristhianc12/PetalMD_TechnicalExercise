using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.DTO.Core.ResponseObject
{
    public class ResponseBasicDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string id { get; set; }
        [NotMapped]
        public object Data { get; set; }
    }
}
