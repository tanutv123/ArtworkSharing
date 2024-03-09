using BusinessObject.Entities;
using DataAccess.Data;
using DataAccess.Management;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Presentation.Helpers;
using Presentation.Services;
using Microsoft.Extensions.DependencyInjection;
using Presentation.SignalR;
using Repository;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
{
    options.Conventions.AddPageRoute("/Account/LoginPage", "");
});
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddIdentityCore<AppUser>(opt =>
{
	opt.Password.RequiredLength = 5;

	opt.User.RequireUniqueEmail = true;
})
	.AddRoles<AppRole>()
	.AddRoleManager<RoleManager<AppRole>>()
	.AddEntityFrameworkStores<DataContext>()
	.AddSignInManager<SignInManager<AppUser>>()
	.AddDefaultTokenProviders();
builder.Services.AddAuthentication(options =>
{
	options.DefaultScheme = IdentityConstants.ApplicationScheme;
	options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
}).AddIdentityCookies();
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
	options.AddPolicy("RequireManagerRole", policy => policy.RequireRole("Manager"));
	options.AddPolicy("RequireArtistRole", policy => policy.RequireRole("Artist"));
    options.AddPolicy("RequireAdminManagerRole", policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole("Admin") || context.User.IsInRole("Manager")));
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1800);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSignalR();

builder.Services.AddScoped<UserManagement>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<CommissionManagement>();
builder.Services.AddScoped<ICommissionRepository, CommissionRepository>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<ArtworkManagement>();
builder.Services.AddScoped<IArtworkRepository, ArtworkRepository>();
builder.Services.AddScoped<GenreManagement>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddSingleton<PresenceTracker>();

builder.Services.AddOptions();
var mailsettings = builder.Configuration.GetSection("MailSettings");
builder.Services.Configure<MailSettings>(mailsettings);
builder.Services.AddTransient<IEmailSender, MailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapRazorPages();
app.MapControllers();

app.MapHub<PresenceHub>("/presensehub");
app.MapHub<CommissionHub>("/commissionhub");

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
	var context = services.GetRequiredService<DataContext>();
	var userManager = services.GetRequiredService<UserManager<AppUser>>();
	var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
	await context.Database.MigrateAsync();
	await Seed.SeedUser(userManager, roleManager);
	await Seed.SeedArtwork(context);
	await Seed.SeedCommissionStatus(context);
	await Seed.SeedPurchaseData(context);
}
catch (Exception ex)
{
	var logger = services.GetRequiredService<ILogger<Program>>();
	logger.LogError(ex, "An error while seeding data");
}

app.Run();
