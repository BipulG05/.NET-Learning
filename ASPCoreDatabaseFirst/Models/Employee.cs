using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASPCoreDatabaseFirst.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public int Age { get; set; }

    public string Position { get; set; } = null!;

    //[Range(1000, int.MaxValue, ErrorMessage = "Salary must be at least 1000.")]
    public decimal Salary { get; set; }

    public string? Active { get; set; }
}
