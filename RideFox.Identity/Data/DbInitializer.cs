using Microsoft.EntityFrameworkCore;

namespace RideFox.Identity.Data;

public class DbInitializer
{
	public static void Initialize(AuthDbContext context) => context.Database.Migrate();
}
