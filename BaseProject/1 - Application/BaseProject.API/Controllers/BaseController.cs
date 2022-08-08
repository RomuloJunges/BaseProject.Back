using BaseProject.Domain.Entities;
using BaseProject.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController : Controller
    {
        private readonly DefaultResponse _response;

        public BaseController()
        {
            _response = new DefaultResponse();
        }

        protected ActionResult CreateResponse(INotificationService notification, object data)
        {
            _response.Data = data;
            _response.Success = !notification.HasNotifications;
            _response.Messages = notification.GetNotifications();

            if (!_response.Success) return BadRequest(_response);

            return Ok(_response);
        }
    }
}