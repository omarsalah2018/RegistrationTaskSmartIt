using FluentValidation;
using RegistrationTask.ViewModels;

namespace Core.Application.Validators
{
    internal class AdduserValidator : AbstractValidator<AddUserVm>
    {
        public AdduserValidator() 
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid email");
           

        }
    }
}
