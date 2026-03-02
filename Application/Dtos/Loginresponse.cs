using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class Loginresponse
    {
        public string Token { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
