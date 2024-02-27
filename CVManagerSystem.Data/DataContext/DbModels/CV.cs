using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVManagerSystem.Data.DataContext.DbModels
{
    public class CV
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public int PersonalInformationId { get; set; }
        public virtual PersonalInformation? PersonalInformation  { get; set; }
        public int ExperienceInformationId { get; set; }
        public virtual ExperienceInformation? ExperienceInformation { get; set; }
    }
}
