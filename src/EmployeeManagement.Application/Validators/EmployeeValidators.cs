using EmployeeManagement.Application.DTOs;
using FluentValidation;

namespace EmployeeManagement.Application.Validators;

public class CreateEmployeeDtoValidator : AbstractValidator<CreateEmployeeDto>
{
    public CreateEmployeeDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(200);
        RuleFor(x => x.BaseSalary).GreaterThan(0);
        RuleFor(x => x.DepartmentId).GreaterThan(0);
    }
}

public class UpdateEmployeeDtoValidator : AbstractValidator<UpdateEmployeeDto>
{
    public UpdateEmployeeDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(200);
        RuleFor(x => x.BaseSalary).GreaterThan(0);
        RuleFor(x => x.DepartmentId).GreaterThan(0);
    }
}
