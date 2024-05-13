using System.ComponentModel;
using System.Text.Json.Serialization;
using EFCore.Models.Common;
using EFCore.Models.Models;

namespace EFCore.Repositories.DTOs;

public class EmployeeCreateDto
{
    /// <summary>
    ///JoinedDate format MM/dd/yyyy
    /// </summary>
    public string? Name { get; set; }
    
    public Guid DepartmentId { get; set; }
    
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    [Description("format dd/MM/yyyy")]
    public DateOnly JoinedDate { get; set; }
    
    public EmployeeCreateDto()
    {
        
    }
    public EmployeeCreateDto(Employee employee)
    {
        Name = employee.Name;
        DepartmentId = employee.DepartmentId;
        JoinedDate = employee.JoinedDate;
    }
}