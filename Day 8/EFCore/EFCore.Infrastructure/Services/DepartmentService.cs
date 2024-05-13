using EFCore.Models.Models;
using EFCore.Repositories.DTOs;
using EFCore.Repositories.Interfaces;

namespace EFCore.Services.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }
    public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
    {
        var departments = (await _departmentRepository.GetAllAsync()).Select(department => new DepartmentDto(department));
        return departments;
    }

    public async Task<DepartmentDto> GetByIdAsync(Guid id)
    {
        var department = await _departmentRepository.GetByIdAsync(id);
        var departmentDto = new DepartmentDto(department);
        return departmentDto;
    }

    public async Task<DepartmentCreateDto> AddAsync(DepartmentCreateDto objModel)
    {
        var department = new Department
        {
            Name = objModel.Name,
        };
        await Task.Run(() => _departmentRepository.AddAsync(department));
        return objModel;
    }

    public async Task<DepartmentCreateDto> UpdateAsync(Guid id,DepartmentCreateDto objModel)
    {
        var department = new Department
        {
            Id = id,
            Name = objModel.Name
        };
        await Task.Run(() => _departmentRepository.UpdateAsync(department));
        return objModel;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var department = new Department
        {
            Id = id
        };
        await Task.Run(() => _departmentRepository.DeleteAsync(department));
        return true;
    }
}