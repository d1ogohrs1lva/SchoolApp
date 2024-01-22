using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolApp.SchoolModels;

public partial class Person
{
    public int Id { get; set; }

    [DisplayName("First Name")]
    [StringLength(50, ErrorMessage = "O primeiro nome deve ter até 50 caracteres")]
    public string FirstName { get; set; } = null!;

    [DisplayName("Last Name")]
    [StringLength(50, ErrorMessage = "O último nome deve ter até 50 caracteres")]
    public string LastName { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public int Roles { get; set; }

    public virtual ICollection<ClassDetail> ClassDetails { get; set; } = new List<ClassDetail>();
    [DisplayName("Role")]
    public virtual Role RolesNavigation { get; set; } = null!;
}
