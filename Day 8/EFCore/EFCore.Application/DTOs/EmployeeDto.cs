using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EFCore.Models.Common;
using EFCore.Models.Models;

namespace EFCore.Repositories.DTOs;

public class EmployeeDto
{
    public Guid Id { get; set; }
    
    public string? Name { get; set; }
    
    public Guid DepartmentId { get; set; }
    
    /// <summary>
    /// format dd/MM/yyyy
    /// </summary>
    [JsonConverter(typeof(DateOnlyJsonConverter))]    
    public DateOnly JoinedDate { get; set; }
    
    public EmployeeDto(Employee employee)
    {
        Id = employee.Id;
        Name = employee.Name;
        DepartmentId = employee.DepartmentId;
        JoinedDate = employee.JoinedDate;
    }
}