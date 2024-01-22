using System;
using System.Collections.Generic;

namespace SchoolApp.SchoolModels;

public partial class Class
{
    public int Id { get; set; }

    public int Student { get; set; }

    public virtual ClassDetail IdNavigation { get; set; } = null!;

    public virtual Person StudentNavigation { get; set; } = null!;
}
