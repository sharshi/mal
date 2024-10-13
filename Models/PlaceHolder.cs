using System;
using System.ComponentModel.DataAnnotations;

namespace Shafeh.Models;
public class PlaceHolder
{
    [Key]
    public int Id { get; set; }
    public required string Title { get; set; }
}
