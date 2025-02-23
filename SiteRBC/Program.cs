using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SiteRBC.Models.AIModels;
using SiteRBC.Services.AccountSevice;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Додаємо підтримку сесій
        builder.Services.AddSession();
        builder.Services.AddHttpClient();
        builder.Services.AddScoped<IUserService, UserService>();
        // Додаємо аутентифікацію через Cookies
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Accounts/SignInAndUpUsers"; // Сторінка входу
                options.LogoutPath = "/Accounts/Logout"; // Сторінка виходу
                options.AccessDeniedPath = "/Home/AccessDenied"; // Сторінка для обмеження доступу
            });
        builder.Services.Configure<OpenAIOptions>(builder.Configuration.GetSection("OpenAI"));
        // Додаємо послуги MVC
        builder.Services.AddControllersWithViews();

        // Підключення до бази даних через контекст
        builder.Services.AddDbContext<SiteRBCContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();

        // Налаштування середовища
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        // Middleware для HTTPS, статичних файлів, маршрутизації
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        // Аутентифікація та авторизація
        app.UseAuthentication(); // Додаємо аутентифікацію
        app.UseAuthorization();

        // Використання сесій
        app.UseSession();

        // Маршрутизація
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Tour}/{id?}");
		app.MapControllerRoute(
			name: "support",
			pattern: "Support/{action=GeneralSupportPage}/{id?}",
			defaults: new { controller = "Support" });

		app.Run();
    }
}
