﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseProject.Domain.DTO.UserDTO;

namespace BaseProject.Service.Interfaces
{
    public interface IAuthService
    {
        Task<UserDTO> Login(LoginUserDTO loginUserDto);
    }
}
