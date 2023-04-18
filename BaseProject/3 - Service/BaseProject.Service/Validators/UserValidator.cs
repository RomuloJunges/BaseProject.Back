using BaseProject.Domain.DTO.UserDTO;
using BaseProject.Domain.Enum;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Service.Validators
{
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator(ValidationType validationType)
        {
            RuleFor(e => e.FirstName).NotEmpty().WithMessage("First name needs to be informed.");
            RuleFor(e => e.LastName).NotEmpty().WithMessage("Last name needs to be informed.");
            RuleFor(e => e.Email).NotEmpty().WithMessage("Email needs to be informed.");


            if (validationType == ValidationType.Insert)
            {
                RuleFor(e => e.Password).NotEmpty().WithMessage("Password needs to be informed.");
                RuleFor(e => e.ConfirmPassword).NotEmpty().WithMessage("Confirm password needs to be informed.");
            }

            if (validationType == ValidationType.Update)
                RuleFor(e => e.Id).NotEmpty().WithMessage("Id needs to be informed.");
        }
    }
}
