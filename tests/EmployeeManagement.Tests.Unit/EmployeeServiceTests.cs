using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Services;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Enums;
using EmployeeManagement.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace EmployeeManagement.Tests.Unit;

public class EmployeeServiceTests
{
    private readonly Mock<IEmployeeRepository> _mockRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly EmployeeService _employeeService;

    public EmployeeServiceTests()
    {
        _mockRepository = new Mock<IEmployeeRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _employeeService = new EmployeeService(_mockRepository.Object, _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnEmployee_WhenEmployeeExists()
    {
        // Arrange
        var employee = new Employee
        {
            Id = 1,
            Name = "John Doe",
            Email = "john@example.com",
            BaseSalary = 5000,
            Position = JobPosition.Developer,
            DepartmentId = 1,
            Department = new Department { Id = 1, Name = "IT" }
        };

        _mockRepository.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(employee);

        // Act
        var result = await _employeeService.GetByIdAsync(1);

        // Assert
        result.Should().NotBeNull();
        result!.Name.Should().Be("John Doe");
        result.Email.Should().Be("john@example.com");
        result.CalculatedSalary.Should().Be(5500m);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenEmployeeDoesNotExist()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Employee?)null);

        // Act
        var result = await _employeeService.GetByIdAsync(999);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateEmployee_AndReturnDto()
    {
        // Arrange
        var createDto = new CreateEmployeeDto
        {
            Name = "Jane Smith",
            Email = "jane@example.com",
            BaseSalary = 6000,
            Position = JobPosition.Manager,
            DepartmentId = 1
        };

        _mockRepository.Setup(r => r.AddAsync(It.IsAny<Employee>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _mockUnitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _employeeService.CreateAsync(createDto);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Jane Smith");
        result.Email.Should().Be("jane@example.com");
        result.BaseSalary.Should().Be(6000);
        _mockRepository.Verify(r => r.AddAsync(It.IsAny<Employee>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_WhenEmployeeExists()
    {
        // Arrange
        var employee = new Employee
        {
            Id = 1,
            Name = "John Doe",
            Email = "john@example.com",
            BaseSalary = 5000,
            Position = JobPosition.Developer,
            DepartmentId = 1
        };

        _mockRepository.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(employee);
        _mockRepository.Setup(r => r.DeleteAsync(1, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _mockUnitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _employeeService.DeleteAsync(1);

        // Assert
        result.Should().BeTrue();
        _mockRepository.Verify(r => r.DeleteAsync(1, It.IsAny<CancellationToken>()), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenEmployeeDoesNotExist()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Employee?)null);

        // Act
        var result = await _employeeService.DeleteAsync(999);

        // Assert
        result.Should().BeFalse();
        _mockRepository.Verify(r => r.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
