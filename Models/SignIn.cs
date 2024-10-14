using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shafeh.Models;

public class SignIn
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int PersonId { get; set; }
    public Person Person { get; set; }

    public int ScheduleId { get; set; }
    public Schedule Schedule { get; set; }

    public DateTime SignInTime { get; set; }
}
