using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class EmailAlreadyExists: Exception
    {
        public int StatusCode { get; } = 400;
        public EmailAlreadyExists(string message) : base(message)
        {
        }

        public EmailAlreadyExists() : base("El correo electrónico ya está registrado.")
        {
        }
    }
}
