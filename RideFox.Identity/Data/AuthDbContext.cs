using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RideFox.Identity.Model;

namespace RideFox.Identity.Data;

public class AuthDbContext : IdentityDbContext<AppUser>
{
	public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.Entity<AppUser>(x => x.ToTable(name: "Users"));
		builder.Entity<IdentityRole>(x => x.ToTable(name: "Roles"));
		builder.Entity<IdentityUserRole<string>>(x => x.ToTable(name: "UserRoles"));
		builder.Entity<IdentityUserClaim<string>>(x => x.ToTable(name: "UserClaims"));
		builder.Entity<IdentityUserLogin<string>>(x => x.ToTable(name: "UserLogins"));
		builder.Entity<IdentityUserToken<string>>(x => x.ToTable(name: "UserTokens"));
		builder.Entity<IdentityRoleClaim<string>>(x => x.ToTable(name: "RoleClaims"));

		builder.ApplyConfiguration(new AppUserConfiguration());
	}
}
