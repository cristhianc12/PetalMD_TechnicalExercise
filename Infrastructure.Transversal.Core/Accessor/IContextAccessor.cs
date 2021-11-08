using Infrastructure.Transversal.Core.UserCache;

using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Transversal.Core.Accessor
{
    public interface IContextAccessor
    {
        string userId { get; }
        //string userName { get; }
        string controller { get; }
        //string language { get; }
        //string idAplicacion { get; }
        DefaultValuesUserDTO oDefaultValuesUserDTO { get; }
    }
}
