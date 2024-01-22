using System;
using System.Collections.Generic;

namespace SchoolApp.SchoolModels;

public partial class Role
{
    public int Id { get; set; }

    public string Label { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
