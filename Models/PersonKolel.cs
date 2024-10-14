using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shafeh.Models;

public class PersonKolel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int PersonId { get; set; }
    public Person Person { get; set; }

    public int KolelId { get; set; }
    public Kolel Kolel { get; set; }
}
