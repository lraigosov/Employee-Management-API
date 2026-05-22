using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DepartmentsController : ControllerBase
{
    private readonly DepartmentService _departmentService;
    private readonly EmployeeService _employeeService;

    public DepartmentsController(DepartmentService departmentService, EmployeeService employeeService)
    {
        _departmentService = departmentService;
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var departments = await _departmentService.GetAllAsync(ct);
        return Ok(departments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var department = await _departmentService.GetByIdAsync(id, ct);
        if (department == null)
            return NotFound();

        return Ok(department);
    }

    [HttpGet("{id}/employees")]
    public async Task<IActionResult> GetEmployees(int id, CancellationToken ct)
    {
        if (!await _departmentService.ExistsAsync(id, ct))
            return NotFound();

        var employees = await _employeeService.GetByDepartmentIdAsync(id, ct);
        return Ok(employees);
    }

    [HttpGet("{id}/total-salary")]
    public async Task<IActionResult> GetTotalSalary(int id, CancellationToken ct)
    {
        if (!await _departmentService.ExistsAsync(id, ct))
            return NotFound();

        var totalSalary = await _departmentService.GetTotalSalaryAsync(id, ct);
        return Ok(new { departmentId = id, totalSalary });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDepartmentDto dto, CancellationToken ct)
    {
        var department = await _departmentService.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = department.Id }, department);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDepartmentDto dto, CancellationToken ct)
    {
        var department = await _departmentService.UpdateAsync(id, dto, ct);
        if (department == null)
            return NotFound();

        return Ok(department);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var result = await _departmentService.DeleteAsync(id, ct);
        if (!result)
            return NotFound();

        return NoContent();
    }
}
