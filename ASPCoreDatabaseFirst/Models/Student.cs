using System;
using System.Collections.Generic;

namespace ASPCoreDatabaseFirst.Models;

public partial class Student
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public int Age { get; set; }

    public int Standard { get; set; }
}
