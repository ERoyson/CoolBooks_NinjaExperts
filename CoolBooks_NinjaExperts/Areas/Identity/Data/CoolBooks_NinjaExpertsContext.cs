using CoolBooks_NinjaExperts.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CoolBooks_NinjaExperts.Models;

namespace CoolBooks_NinjaExperts.Data;

public class CoolBooks_NinjaExpertsContext : IdentityDbContext<UserInfo>
{
    public CoolBooks_NinjaExpertsContext(DbContextOptions<CoolBooks_NinjaExpertsContext> options)
        : base(options)
    {
    }
    
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
    
    
    public DbSet<CoolBooks_NinjaExperts.Models.Books> Books { get; set; }
    public DbSet<CoolBooks_NinjaExperts.Models.Authors> Authors { get; set; }
    public DbSet<CoolBooks_NinjaExperts.Models.Genres> Genres { get; set; }
    public DbSet<CoolBooks_NinjaExperts.Models.Images> Images { get; set; }
    public DbSet<CoolBooks_NinjaExperts.Models.Reviews> Reviews { get; set; }
    public DbSet<UserInfo> UserInfo { get; set; }
}
