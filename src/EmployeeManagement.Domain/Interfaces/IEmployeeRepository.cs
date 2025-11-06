using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Domain.Interfaces;

public interface IEmployeeRepository
{
    Task<Employee?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<Employee>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<Employee>> GetByDepartmentIdAsync(int departmentId, CancellationToken ct = default);
    Task AddAsync(Employee employee, CancellationToken ct = default);
    Task UpdateAsync(Employee employee, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}
