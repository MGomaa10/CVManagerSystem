using CVManagerSystem.Core.Base;
using CVManagerSystem.Core.Dtos;
using CVManagerSystem.Data.DataContext.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVManagerSystem.Services.Interfaces
{
    public interface ICVServices
    {
        Task<List<CV>> GetCVsListAsync();
        Task<List<CV>> GetCVsFilterByCityAsync(string cityName);
        Task<CV> GetCVByIdAsync(int Id);
        Task<IResponseDto> AddCVAsync(CVDto cv);
        Task<IResponseDto> EditCVAsync(CVDto cv, int Id);
        Task<IResponseDto> DelelteCVAsync(int Id);
    }
}
