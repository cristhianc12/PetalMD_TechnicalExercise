using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Transversal.Core
{
    public static class RegularExpression
    {
        public const string CorreoElectronico = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
    }
}
