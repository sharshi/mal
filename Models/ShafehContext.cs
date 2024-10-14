using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Shafeh.Models;

namespace Shafeh.Models;
public class ShafehContext : IdentityDbContext
{
    public ShafehContext(DbContextOptions<ShafehContext> options) : base(options)
    {

    }

    public DbSet<Kolel> Kolels { get; set; }
}
