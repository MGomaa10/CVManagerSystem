﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVManagerSystem.Data.DataContext.DbModels
{
    public class ExperienceInformation
    {
        [Key]
        public int ID { get; set; }
        [Required, MaxLength(20, ErrorMessage = "maximum {20} characters allowed")]
        public string CompanyName { get; set; } = null!;
        public string CityName { get; set; } = null!;
        public string CompanyField { get; set; } = null!;
    }
}
