using System.ComponentModel;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using OllamaSemanticApi.Models;

namespace OllamaSemanticApi.Plugins;

public class TeachersPlugin(DatabaseTestContext context)
{
    [KernelFunction, Description("Show me all teachers")]
    public async Task<List<TeachersDTO>> ShowAllTeachers()
    {
        var allTeachers = await context.Teachers.ToListAsync();
        var teachers = allTeachers.Select(x => new TeachersDTO(){ Name = x.FullName }).ToList();
        return teachers;
    }
    
    [KernelFunction("add_new_teacher"), Description("Add a new teacher")]
    public async Task AddNewTeacher(string name)
    {
        await context.Teachers.AddAsync(new Teacher() { TeacherId = Guid.NewGuid(), FullName = name });
        
        await context.SaveChangesAsync();
    }
}

public record TeachersDTO
{
    [JsonPropertyName(("FullName"))]
    public string Name { get; init; }
    
}

