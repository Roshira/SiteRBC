using Microsoft.EntityFrameworkCore;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.Services.AddSession();

		// Додайте послуги MVC
		builder.Services.AddControllersWithViews();
		// Add services to the container.
		builder.Services.AddControllersWithViews();

		builder.Services.AddDbContext<ReadyProductContext>(options =>
			options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();
		
		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseSession();

		app.UseAuthorization();

		app.MapControllerRoute(
			name: "default",
            pattern: "{controller=Home}/{action=Tour}/{id?}");

		app.Run();
	}
}