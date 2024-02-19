using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideFox.Identity.Model;

namespace RideFox.Identity.Data;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
	public void Configure(EntityTypeBuilder<AppUser> builder) => builder.HasKey(x => x.Id);
}
