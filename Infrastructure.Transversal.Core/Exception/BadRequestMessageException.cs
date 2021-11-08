using System;

namespace Infrastructure.Transversal.Core.Exceptions
{
    /// <summary>
    /// Se utiliza para devolver mensajes con el código 430 http
    /// </summary>
    public class BadRequestMessageException : Exception
    {
        public BadRequestMessageException(string message) : base(message)
        {

        }
    }
}
