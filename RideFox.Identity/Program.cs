using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RideFox.Identity.Data;
using RideFox.Identity.Model;

namespace RideFox.Identity;

public class Program
{
	public IConfiguration AppConfiguration { get; }

	public Program(IConfiguration configuration)
	{
		AppConfiguration = configuration;
	}

	public void Main(string[] args)
	{
		var connectionString = AppConfiguration.GetValue<string>("DbConnection");
		var builder = WebApplication.CreateBuilder(args);

		#region ���������� � ������������ �������� 

		// ����������� DbContext
		builder.Services.AddDbContext<AuthDbContext>(options =>
			{
				options.UseNpgsql(connectionString);
			});

		// ����������� � ������������ Identity
		builder.Services.AddIdentity<AppUser, IdentityRole>(config =>
		{
			// ���������������� ���������
			config.Password.RequiredLength = 4;
			config.Password.RequireDigit = false;
			config.Password.RequireNonAlphanumeric = false;
			config.Password.RequireUppercase = false;
		})
			.AddEntityFrameworkStores<AuthDbContext>()
			.AddDefaultTokenProviders();

		// ����������� IdentityServer4 � ���������� ��������� ������������
		builder.Services.AddIdentityServer()
			.AddAspNetIdentity<AppUser>()
			.AddInMemoryApiResources(Configuration.ApiResources)
			.AddInMemoryIdentityResources(Configuration.IdentityResources)
			.AddInMemoryApiScopes(Configuration.ApiScopes)
			.AddInMemoryClients(Configuration.Clients)
			.AddDeveloperSigningCredential(); // ���������������� ����������

		// ������������ Cookie ��������
		builder.Services.ConfigureApplicationCookie(config =>
		{
			config.Cookie.Name = "RideFox.Identity.Cookie";
			config.LoginPath = "/Auth/Login";
			config.LogoutPath = "/Auth/Logout";
		});

		#endregion

		var app = builder.Build();
		using(var scope = app.Services.CreateScope())
		{
			var serviceProvider = scope.ServiceProvider;

			try
			{
				var context = serviceProvider.GetRequiredService<AuthDbContext>();
				DbInitializer.Initialize(context);
			}
			catch(Exception ex)
			{
				var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
				logger.LogError(ex, "An error occurred while app initialization");
			}
		}

		app.UseIdentityServer();
		app.MapGet("/", () => "Hello World!");

		app.Run();
	}
}
