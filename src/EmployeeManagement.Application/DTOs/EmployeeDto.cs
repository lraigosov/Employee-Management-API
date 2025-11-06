using EmployeeManagement.Domain.Enums;

namespace EmployeeManagement.Application.DTOs;

public class EmployeeDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public decimal BaseSalary { get; set; }
    public JobPosition Position { get; set; }
    public int DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public decimal CalculatedSalary { get; set; }
}

public class CreateEmployeeDto
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public decimal BaseSalary { get; set; }
    public JobPosition Position { get; set; }
    public int DepartmentId { get; set; }
}

public class UpdateEmployeeDto
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public decimal BaseSalary { get; set; }
    public JobPosition Position { get; set; }
    public int DepartmentId { get; set; }
}
