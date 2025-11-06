using EmployeeManagement.Domain.Enums;

namespace EmployeeManagement.Domain.Entities;

public class Employee
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public decimal BaseSalary { get; set; }
    public JobPosition Position { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;
    
    public decimal CalculateSalary() => Position switch
    {
        JobPosition.Developer => BaseSalary * 1.10m,
        JobPosition.Manager => BaseSalary * 1.20m,
        _ => BaseSalary
    };
}
