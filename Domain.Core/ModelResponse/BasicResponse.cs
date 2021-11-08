using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Core.ModelResponse
{
    public class BasicResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string id { get; set; }
    }
}
