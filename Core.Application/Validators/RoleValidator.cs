using Core.Application.Dtos;
using FluentValidation;

namespace Core.Application.Validators
{
    public class RoleValidator : AbstractValidator<RoleDto>
    {
        public RoleValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.RoleDescription).NotEmpty().WithMessage("Description is required");
        }

    }
}
