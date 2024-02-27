using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVManagerSystem.Data.DataContext.DbIdentity
{
    public class ApplicationUser : IdentityUser
    {
        public string? Address { get; set; }
        public string? Country { get; set; }
    }
}
