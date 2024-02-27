using CVManagerSystem.Core.Base;
using CVManagerSystem.Core.Dtos;
using CVManagerSystem.Data.DataContext.DbModels;
using CVManagerSystem.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVManagerSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CVController : BaseController
    {
        private readonly ICVServices _cvServices;
        public CVController(
           ICVServices cvServices,
           IResponseDto response,
           IHttpContextAccessor httpContextAccessor) : base(response, httpContextAccessor)
        {
            _cvServices = cvServices;
        }

        [HttpPost]
        public async Task<IResponseDto> CreateCV([FromForm] CVDto options)
        {
            var respons = await _cvServices.AddCVAsync(options);
            return respons;
        }

        [HttpGet]
        public async Task<List<CV>> GetAllCVs()
        {
            var respons = await _cvServices.GetCVsListAsync();
            return respons;
        }

        [HttpGet("{Id}")]
        public async Task<CV> GetOneCV(int Id)
        {
            var respons = await _cvServices.GetCVByIdAsync(Id);
            return respons;
        }

        [HttpPut("{Id}")]
        public async Task<IResponseDto> EditCV([FromForm] CVDto options, int Id)
        {
            var respons = await _cvServices.EditCVAsync(options, Id);
            return respons;
        }

        [HttpDelete("{Id}")]
        public async Task<IResponseDto> DeleteCV(int Id)
        {
            var respons = await _cvServices.DelelteCVAsync(Id);
            return respons;
        }

        [HttpGet("GetCVsFilteredByCity")]
        public async Task<List<CV>> GetCVsFilteredByCity([FromQuery]string city)
        {
            var respons = await _cvServices.GetCVsFilterByCityAsync(city);
            return respons;
        }
    }
}
