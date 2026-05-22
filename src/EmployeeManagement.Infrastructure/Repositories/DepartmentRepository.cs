using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly ApplicationDbContext _context;

    public DepartmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Department?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.Departments
            .AsNoTracking()
            .Include(d => d.Employees)
            .FirstOrDefaultAsync(d => d.Id == id, ct);
    }

    public async Task<IEnumerable<Department>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Departments
            .AsNoTracking()
            .Include(d => d.Employees)
            .ToListAsync(ct);
    }

    public Task<bool> ExistsAsync(int id, CancellationToken ct = default)
    {
        return _context.Departments.AnyAsync(d => d.Id == id, ct);
    }

    public async Task AddAsync(Department department, CancellationToken ct = default)
    {
        await _context.Departments.AddAsync(department, ct);
    }

    public Task UpdateAsync(Department department, CancellationToken ct = default)
    {
        _context.Departments.Update(department);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id, ct);
        if (department != null)
        {
            _context.Departments.Remove(department);
        }
    }
}
