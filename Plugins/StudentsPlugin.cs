using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using OllamaSemanticApi.Models;

namespace OllamaSemanticApi.Plugins;

public class StudentsPlugin(DatabaseTestContext context)
{
    [KernelFunction("get_all_students")]
    [Description("Return all students")]
    public async Task<IEnumerable<Student>> GetStudents()
    {
        return await context.Students.ToListAsync();
    }
    
    [KernelFunction("insert_student_by_fullname")]
    [Description("Add or Insert a new student to the database and return it")]
    public async Task<Student> AddStudent(string fullName)
    {
        var student = new Student
        {
            StudentId = Guid.NewGuid(),
            FullName = fullName,
            CreatedAt = DateTime.Now,
        };
        await context.Students.AddAsync(student);
        
        await context.SaveChangesAsync();

        return student;
    }

    [KernelFunction("remove_student_by_name")]
    [Description("Remove student by full name")]
    public async Task RemoveStudent(string fullName)
    {
        var student = await context.Students.FirstAsync(x => x.FullName == fullName);
        
        context.Students.Remove(student);
        
        await context.SaveChangesAsync();
    }
    
    [KernelFunction("get_last_student_added")]
    [Description("Get the last added student and return it")]
    public async Task<Student> LastStudent()
    {
        var student = await context.Students.OrderBy(x => x.CreatedAt).LastAsync();
        
        return student;
    }


    [KernelFunction("add_list_students")]
    [Description("Add a list of students")]
    public async Task AddStudents(string students)
    {
        string list = students.Replace("[", "").Replace("]", "");
        var studentsArray = list.Split(',');
        
        List<Student> studentList = studentsArray.Select(x => new Student
        {
            StudentId = Guid.NewGuid(),
            FullName = x, 
            CreatedAt = DateTime.Now
        }).ToList();
        
        await context.Students.AddRangeAsync(studentList);
        
        await context.SaveChangesAsync();
    }
}