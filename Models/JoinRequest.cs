using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shafeh.Models;

public class JoinRequest
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string UserId { get; set; }
    public int KolelId { get; set; }
    public DateTime RequestDate { get; set; }
    public string Status { get; set; } // e.g., Pending, Approved, Rejected
}
