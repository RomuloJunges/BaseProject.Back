using BaseProject.Domain.DTO.UserDTO;
using BaseProject.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.API.Controllers
{

    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserDTO>> Post([FromBody] CreateUserDTO userDto)
        {
            return CreateResponse(_userService, await _userService.Add(userDto));
        }
    }
}