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

        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> Get()
        {
            return await _userService.GetAll();
        }

        [HttpGet("email")]
        public async Task<ActionResult<UserDTO>> Get(string email)
        {
            return await _userService.GetByEmail(email);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserDTO>> Post([FromBody] UserDTO userDto)
        {
            return await _userService.Add(userDto);
        }

        [HttpPut]
        public async Task<ActionResult<UserDTO>> Put([FromBody] UserDTO userDto)
        {
            return await _userService.Update(userDto);
        }

        [HttpDelete("id")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            return await _userService.Delete(id);
        }
    }
}