using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shafeh.Models;

public class Person
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public required string Name { get; set; }

    public ICollection<PersonKolel> PersonKollels { get; set; } = new List<PersonKolel>();

    public ICollection<SignIn> SignIns { get; set; } = new List<SignIn>();
}
