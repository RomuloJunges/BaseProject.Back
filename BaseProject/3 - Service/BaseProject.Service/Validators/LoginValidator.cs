using BaseProject.Domain.DTO.UserDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Service.Validators
{
    public class LoginValidator : AbstractValidator<LoginUserDTO>
    {
        public LoginValidator()
        {
            RuleFor(e => e.Email).NotEmpty().WithMessage("Email needs to be informed.");
            RuleFor(e => e.Password).NotEmpty().WithMessage("Password needs to be informed.");
        }
    }
}
