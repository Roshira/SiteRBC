using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // ������ �������� ����
        builder.Services.AddSession();

        // ������ �������������� ����� Cookies
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Accounts/SignInAndUpUsers"; // ������� �����
                options.LogoutPath = "/Accounts/Logout"; // ������� ������
                options.AccessDeniedPath = "/Home/AccessDenied"; // ������� ��� ��������� �������
            });

        // ������ ������� MVC
        builder.Services.AddControllersWithViews();

        // ϳ��������� �� ���� ����� ����� ��������
        builder.Services.AddDbContext<SiteRBCContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();

        // ������������ ����������
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        // Middleware ��� HTTPS, ��������� �����, �������������
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        // �������������� �� �����������
        app.UseAuthentication(); // ������ ��������������
        app.UseAuthorization();

        // ������������ ����
        app.UseSession();

        // �������������
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
