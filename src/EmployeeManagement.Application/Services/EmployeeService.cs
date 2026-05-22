using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;

namespace EmployeeManagement.Application.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeService(
        IEmployeeRepository employeeRepository,
        IDepartmentRepository departmentRepository,
        IUnitOfWork unitOfWork)
    {
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<EmployeeDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var employee = await _employeeRepository.GetByIdAsync(id, ct);
        return employee == null ? null : MapToDto(employee);
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllAsync(CancellationToken ct = default)
    {
        var employees = await _employeeRepository.GetAllAsync(ct);
        return employees.Select(MapToDto);
    }

    public async Task<IEnumerable<EmployeeDto>> GetByDepartmentIdAsync(int departmentId, CancellationToken ct = default)
    {
        var employees = await _employeeRepository.GetByDepartmentIdAsync(departmentId, ct);
        return employees.Select(MapToDto);
    }

    public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto, CancellationToken ct = default)
    {
        if (!await _departmentRepository.ExistsAsync(dto.DepartmentId, ct))
        {
            throw new ArgumentException("Department does not exist.");
        }

        var employee = new Employee
        {
            Name = dto.Name,
            Email = dto.Email,
            BaseSalary = dto.BaseSalary,
            Position = dto.Position,
            DepartmentId = dto.DepartmentId
        };

        await _employeeRepository.AddAsync(employee, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return MapToDto(employee);
    }

    public async Task<EmployeeDto?> UpdateAsync(int id, UpdateEmployeeDto dto, CancellationToken ct = default)
    {
        var employee = await _employeeRepository.GetByIdAsync(id, ct);
        if (employee == null) return null;

        if (!await _departmentRepository.ExistsAsync(dto.DepartmentId, ct))
        {
            throw new ArgumentException("Department does not exist.");
        }

        employee.Name = dto.Name;
        employee.Email = dto.Email;
        employee.BaseSalary = dto.BaseSalary;
        employee.Position = dto.Position;
        employee.DepartmentId = dto.DepartmentId;

        await _employeeRepository.UpdateAsync(employee, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return MapToDto(employee);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var employee = await _employeeRepository.GetByIdAsync(id, ct);
        if (employee == null) return false;

        await _employeeRepository.DeleteAsync(id, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return true;
    }

    private static EmployeeDto MapToDto(Employee employee)
    {
        return new EmployeeDto
        {
            Id = employee.Id,
            Name = employee.Name,
            Email = employee.Email,
            BaseSalary = employee.BaseSalary,
            Position = employee.Position,
            DepartmentId = employee.DepartmentId,
            DepartmentName = employee.Department?.Name,
            CalculatedSalary = employee.CalculateSalary()
        };
    }
}
