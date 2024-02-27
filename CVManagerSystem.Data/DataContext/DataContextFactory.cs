using CVManagerSystem.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CVManagerSystem.Data.DataContext
{
    public class DataContextFactory: AppDataContext
    {
        public DataContextFactory()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetStrigConnection.GetString("CVManageSystemContext"), b => b.MigrationsAssembly("CVManagerSystem.Data"));
        }
    }
}
