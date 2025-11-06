using EmployeeManagement.Application.DTOs;
using FluentValidation;

namespace EmployeeManagement.Application.Validators;

public class CreateDepartmentDtoValidator : AbstractValidator<CreateDepartmentDto>
{
    public CreateDepartmentDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
    }
}

public class UpdateDepartmentDtoValidator : AbstractValidator<UpdateDepartmentDto>
{
    public UpdateDepartmentDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
    }
}
