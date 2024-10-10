using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shafeh.Models;

namespace Shafeh.Models;
public class ShafehContext : DbContext
{
    public ShafehContext(DbContextOptions<ShafehContext> options) : base(options)
    {

    }

    public DbSet<PlaceHolder> PlaceHolder { get; set; }
}