using System.ComponentModel;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using OllamaSemanticApi.Models;

namespace OllamaSemanticApi.Plugins;

public class ClassesPlugin(DatabaseTestContext context)
{
    [KernelFunction ,Description("Show all classes available")]
    public async Task<List<ClassDTO>> ShowAllClassesAvailable()
    {
        var allClasses = await context.Classes.ToListAsync();
        
        return allClasses.Select(x => new ClassDTO(x.ClassId, x.Description)).ToList();
    }
    
}

public record ClassDTO(Guid ClassId, string Description)
{
    [JsonPropertyName("ClassId")]
    public Guid ClassId { get; } = ClassId;
    
    [JsonPropertyName("Description")]
    public string Description { get; } = Description;
}