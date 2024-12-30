using System;
using System.Collections.Generic;

namespace OllamaSemanticApi.Models;

public class StudentClass
{
    public Guid StudentClassesId { get; set; }

    public Guid StudentId { get; set; }

    public Guid ClassId { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
