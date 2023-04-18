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
    }
}