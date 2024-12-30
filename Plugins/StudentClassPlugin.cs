using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using OllamaSemanticApi.Models;

namespace OllamaSemanticApi.Plugins;

public class StudentClassPlugin(DatabaseTestContext testContext)
{
    [KernelFunction, Description("Return the list of students and their classes")]
    public async Task<List<StudentClassesDTO>> GetStudentClasses()
    {
        var studentClasses =  from s in testContext.StudentClasses
                select new StudentClassesDTO
                {
                    ClassName = s.Class.Description,
                    Name = s.Student.FullName
                };
        
        var result = await studentClasses.ToListAsync();
        return result;
    }
}

public record StudentClassesDTO
{
    public string Name { get; init; }
    public string ClassName { get; init; }
}