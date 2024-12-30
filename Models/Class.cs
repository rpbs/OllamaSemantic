using System;
using System.Collections.Generic;

namespace OllamaSemanticApi.Models;

public partial class Class
{
    public Guid ClassId { get; set; }

    public string Description { get; set; } = null!;

    public Guid TeacherId { get; set; }

    public virtual ICollection<StudentClass> StudentClasses { get; set; } = new List<StudentClass>();

    public virtual Teacher Teacher { get; set; } = null!;
}
