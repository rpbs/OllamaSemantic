using System;
using System.Collections.Generic;

namespace OllamaSemanticApi.Models;

public class Teacher
{
    public Guid TeacherId { get; set; }

    public String FullName { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
