using Infrastructure.Transversal.Core.UserCache;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Transversal.Core.Accessor
{
	public class ContextAccessor : IContextAccessor
	{
		private IHttpContextAccessor _httpContextAccessor;
		private IMemoryCache _cache;
		public ContextAccessor(IHttpContextAccessor httpContextAccessor, IMemoryCache cache)
		{
			_httpContextAccessor = httpContextAccessor;
			_cache = cache;
		}

		public string controller { get => ((Microsoft.AspNetCore.Http.Internal.DefaultHttpRequest)((DefaultHttpContext)_httpContextAccessor.HttpContext).Request).Path; }
		//public string language { get => _httpContextAccessor.HttpContext.User.Claims.First(claim => claim.Type == "Idioma").Value; }
		public string userId { get => _httpContextAccessor.HttpContext.User.Claims.First(claim => claim.Type == "NameId").Value; }
		//public string userName { get => _httpContextAccessor.HttpContext.User.Identity.Name; }
		//public string idAplicacion { get => _httpContextAccessor.HttpContext.User.Claims.First(claim => claim.Type == "IdAplicacion").Value; }
		public DefaultValuesUserDTO oDefaultValuesUserDTO { get => _cache.Get<DefaultValuesUserDTO>("User_" + _httpContextAccessor.HttpContext.User.Claims.First(claim => claim.Type == "NameId").Value) != null ? _cache.Get<DefaultValuesUserDTO>("User_" + _httpContextAccessor.HttpContext.User.Claims.First(claim => claim.Type == "NameId").Value) : new DefaultValuesUserDTO(); }
	}
}
