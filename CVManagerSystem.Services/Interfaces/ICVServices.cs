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
        Task<CV> GetCVByIdAsync(int Id);
        Task<IResponseDto> AddAsync(CVDto cv);
        Task<IResponseDto> EditAsync(CVDto cv, int Id);
        Task<IResponseDto> DelelteCV(int Id);
    }
}
