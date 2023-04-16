using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BaseProject.Domain.DTO.UserDTO;
using BaseProject.Service.Interfaces;
using BaseProject.Service.Validators;
using FluentValidation;

namespace BaseProject.Service.Services
{
    public class AuthService : IAuthService
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
            LoginValidator validator = new();
            await validator.ValidateAndThrowAsync(loginUserDto);

            var user = await _userService.GetByEmail(loginUserDto.Email);

            if (user == null)
                throw new Exception("User or password invalid.");

            var result = await _userService.CheckUserPassword(loginUserDto);

            if (!result.Succeeded)
                throw new Exception("User or password invalid.");

            user.Token = await _tokenService.CreateToken(user);

            return _mapper.Map<TokenUserDTO>(user);

        }
    }
}
