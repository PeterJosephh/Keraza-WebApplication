using KerazaWebApplication.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KerazaWebApplication
{

    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresLower),
                Description = "Your password must contain at least one lowercase letterrrrrrrrrrrrr."
            };
        }
    }




    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();




            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;              // Requires at least one digit (0-9)
                options.Password.RequiredLength = 4;              // Minimum password length
                options.Password.RequireNonAlphanumeric = false;   // Requires at least one non-alphanumeric character
                options.Password.RequireUppercase = false;         // Requires at least one uppercase letter (A-Z)
                options.Password.RequireLowercase = true;         // Requires at least one lowercase letter (a-z)
                options.Password.RequiredUniqueChars = 0;         // Requires a minimum number of unique characters
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddScoped<IdentityErrorDescriber, CustomIdentityErrorDescriber>();


            builder.Services.AddControllersWithViews();

            // Register GoogleDriveService as a singleton or scoped service
            builder.Services.AddSingleton<GoogleDriveService>(); // Or AddScoped<GoogleDriveService>();

            // Register other services
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
