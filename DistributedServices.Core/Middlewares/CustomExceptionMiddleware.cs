using Infrastructure.Transversal.Core.Exceptions;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using System;
using System.Net;
using System.Threading.Tasks;

namespace DistribuitedServices.Core.Middlewares
{
    public class CustomExceptionMiddleware
    {
		private readonly RequestDelegate _next;

		private readonly ILogger _logger;

		public CustomExceptionMiddleware(RequestDelegate next, ILogger<HttpContext> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next.Invoke(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			var response = context.Response;

			int statusCode;
			string message;
			string description;

			message = ex.Message;

			if (ex is BadRequestMessageException)
			{
				statusCode = 430;
				description = "Internal Message";

				//Aquí va el logger de SQL

				_logger.LogWarning(new EventId(statusCode, description),
			 ex,
			 "FDIM_API Warning \n Path: {0} \n Method: {1} \n User: {2}",
			 new object[]
			 { context.Request.Path, context.Request.Method, context.User.Identity.IsAuthenticated ? context.User.Identity.Name : "N/A" });
			}
			else
			{
				statusCode = (int)HttpStatusCode.InternalServerError;
				statusCode = (int)HttpStatusCode.BadRequest;
				description = "Internal Error";

				//Aquí va el logger de SQL
				_logger.LogError(new EventId(statusCode, description),
			 ex,
			 "FDIM_API Error \n Path: {0} \n Method: {1} \n User: {2}",
			 new object[]
			 { context.Request.Path, context.Request.Method, context.User.Identity.IsAuthenticated ? context.User.Identity.Name : "N/A" });
			}

			response.ContentType = "application/json";
			response.StatusCode = statusCode;

			await response.WriteAsync(JsonConvert.SerializeObject(new CustomErrorResponse
			{
				Message = message,
				Description = description
			}));
		}
	}
}
