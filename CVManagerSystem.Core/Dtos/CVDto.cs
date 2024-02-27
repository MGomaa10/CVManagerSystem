using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVManagerSystem.Core.Dtos
{
    public class CVDto
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required, MaxLength(20, ErrorMessage = "maximum {20} characters allowed")]
        public string CompanyName { get; set; } = null!;
        public string CompanyCityName { get; set; } = null!;
        public string CompanyField { get; set; } = null!;
        [Required]
        public string FullName { get; set; } = null!;
        public string CityName { get; set; } = null!;
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [DataType(DataType.PhoneNumber)]
        public string? MobileNumber { get; set; }
    }
}
