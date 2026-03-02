using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class InvalidCredentialsException: Exception
    {
        public int StatusCode { get; } = 401;
        public InvalidCredentialsException() : base("Credenciales inválidas.")
        {
        }
        public InvalidCredentialsException(string message) : base(message)
        {
        }

    }
}
