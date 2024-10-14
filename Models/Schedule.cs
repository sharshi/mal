using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shafeh.Models;

public class Schedule
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int KolelId { get; set; }
    public Kolel Kolel { get; set; }

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public ICollection<SignIn> SignIns { get; set; } = new List<SignIn>();
}
