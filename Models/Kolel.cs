using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shafeh.Models;

public class Kolel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Name { get; set; }

    public ICollection<PersonKolel> PersonKollels { get; set; } = new List<PersonKolel>();

    public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
