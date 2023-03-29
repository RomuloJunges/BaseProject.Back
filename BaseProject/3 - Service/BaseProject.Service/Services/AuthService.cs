using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BaseProject.Domain.DTO.UserDTO;
using BaseProject.Service.Interfaces;

namespace BaseProject.Service.Services
{
    public class AuthService : NotificationService, IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;


        public AuthService(IUserService userService, ITokenService tokenService, IMapper mapper)
        {
            _userService = userService;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<TokenUserDTO> Login(LoginUserDTO loginUserDto)
        {
            try
            {
                var user = await _userService.GetByEmail(loginUserDto.Email);
                if (user == null)
                {
                    AddNotification("User or password invalid");
                    return null;
                }

                var result = await _userService.CheckUserPassword(loginUserDto);
                if (!result.Succeeded)
                {
                    AddNotification("User or password invalid");
                    return null;
                }

                user.Token = await _tokenService.CreateToken(user);

                return _mapper.Map<TokenUserDTO>(user);

            }
            catch (Exception e)
            {
                AddNotification("Error on login");
                return null;
            }

        }
    }
}
