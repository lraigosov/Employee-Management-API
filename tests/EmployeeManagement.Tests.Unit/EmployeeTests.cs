using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Enums;
using FluentAssertions;

namespace EmployeeManagement.Tests.Unit;

public class EmployeeTests
{
    [Fact]
    public void CalculateSalary_ForDeveloper_ShouldApply10PercentBonus()
    {
        // Arrange
        var employee = new Employee
        {
            Name = "John Doe",
            Email = "john@example.com",
            BaseSalary = 5000m,
            Position = JobPosition.Developer,
            DepartmentId = 1
        };

        // Act
        var calculatedSalary = employee.CalculateSalary();

        // Assert
        calculatedSalary.Should().Be(5500m);
    }

    [Fact]
    public void CalculateSalary_ForManager_ShouldApply20PercentBonus()
    {
        // Arrange
        var employee = new Employee
        {
            Name = "Jane Smith",
            Email = "jane@example.com",
            BaseSalary = 6000m,
            Position = JobPosition.Manager,
            DepartmentId = 1
        };

        // Act
        var calculatedSalary = employee.CalculateSalary();

        // Assert
        calculatedSalary.Should().Be(7200m);
    }

    [Fact]
    public void CalculateSalary_ForHR_ShouldReturnBaseSalary()
    {
        // Arrange
        var employee = new Employee
        {
            Name = "Bob Johnson",
            Email = "bob@example.com",
            BaseSalary = 4000m,
            Position = JobPosition.HR,
            DepartmentId = 1
        };

        // Act
        var calculatedSalary = employee.CalculateSalary();

        // Assert
        calculatedSalary.Should().Be(4000m);
    }

    [Fact]
    public void CalculateSalary_ForSales_ShouldReturnBaseSalary()
    {
        // Arrange
        var employee = new Employee
        {
            Name = "Alice Williams",
            Email = "alice@example.com",
            BaseSalary = 4500m,
            Position = JobPosition.Sales,
            DepartmentId = 1
        };

        // Act
        var calculatedSalary = employee.CalculateSalary();

        // Assert
        calculatedSalary.Should().Be(4500m);
    }
}
