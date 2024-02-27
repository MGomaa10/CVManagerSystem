using CVManagerSystem.Core.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVManagerSystem.API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {

        private IHttpContextAccessor _httpContextAccessor;
        public IResponseDto _response;

        public BaseController(IResponseDto responseDTO, IHttpContextAccessor httpContextAccessor)
        {
            _response = responseDTO;
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
