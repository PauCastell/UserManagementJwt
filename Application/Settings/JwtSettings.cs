using System.ComponentModel.DataAnnotations;

namespace Api.Configuration
{
    public class JwtSettings
    {
        [Required]
        [MinLength(32)]
        public string SecretKey { get; set; } = null!;
        [Required]
        public string Issuer { get; set; } = null!;
        [Required]
        public string Audience { get; set; } = null!;
        [Range(1,int.MaxValue)]
        public int ExpiryMinutes { get; set; } = 60;
    }
}
