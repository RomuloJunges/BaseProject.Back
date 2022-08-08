using BaseProject.Domain.DTO.UserDTO;
using BaseProject.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.API.Controllers
{
    
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<TokenUserDTO>> Login([FromBody] LoginUserDTO login)
        {
            return CreateResponse(_authService, await _authService.Login(login));
        }
    }
}