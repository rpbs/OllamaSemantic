using System;
using System.Collections.Generic;

namespace OllamaSemanticApi.Models;

public class Student
{
    public Guid StudentId { get; set; }

    public string FullName { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }

    public virtual ICollection<StudentClass> StudentClasses { get; set; } = new List<StudentClass>();
}
