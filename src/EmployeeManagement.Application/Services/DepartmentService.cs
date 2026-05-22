using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;

namespace EmployeeManagement.Application.Services;

public class DepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DepartmentService(
        IDepartmentRepository departmentRepository,
        IEmployeeRepository employeeRepository,
        IUnitOfWork unitOfWork)
    {
        _departmentRepository = departmentRepository;
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<DepartmentDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var department = await _departmentRepository.GetByIdAsync(id, ct);
        return department == null ? null : MapToDto(department);
    }

    public async Task<IEnumerable<DepartmentDto>> GetAllAsync(CancellationToken ct = default)
    {
        var departments = await _departmentRepository.GetAllAsync(ct);
        return departments.Select(MapToDto);
    }

    public Task<bool> ExistsAsync(int id, CancellationToken ct = default)
    {
        return _departmentRepository.ExistsAsync(id, ct);
    }

    public async Task<decimal> GetTotalSalaryAsync(int departmentId, CancellationToken ct = default)
    {
        var employees = await _employeeRepository.GetByDepartmentIdAsync(departmentId, ct);
        return employees.Sum(e => e.CalculateSalary());
    }

    public async Task<DepartmentDto> CreateAsync(CreateDepartmentDto dto, CancellationToken ct = default)
    {
        var department = new Department
        {
            Name = dto.Name
        };

        await _departmentRepository.AddAsync(department, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return MapToDto(department);
    }

    public async Task<DepartmentDto?> UpdateAsync(int id, UpdateDepartmentDto dto, CancellationToken ct = default)
    {
        var department = await _departmentRepository.GetByIdAsync(id, ct);
        if (department == null) return null;

        department.Name = dto.Name;

        await _departmentRepository.UpdateAsync(department, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return MapToDto(department);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var department = await _departmentRepository.GetByIdAsync(id, ct);
        if (department == null) return false;

        await _departmentRepository.DeleteAsync(id, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return true;
    }

    private static DepartmentDto MapToDto(Department department)
    {
        return new DepartmentDto
        {
            Id = department.Id,
            Name = department.Name
        };
    }
}
