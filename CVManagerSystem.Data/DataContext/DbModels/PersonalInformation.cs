using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVManagerSystem.Data.DataContext.DbModels
{
    public class PersonalInformation
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string FullName { get; set; } = null!;
        public string CityName { get; set; } = null!;
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [DataType(DataType.PhoneNumber)]
        public string MobileNumber { get; set; } = null!;

        public static implicit operator PersonalInformation(int v)
        {
            throw new NotImplementedException();
        }
    }
}
