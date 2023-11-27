using LodBasen.Models;
using LodBasen.Services.EFServices;
using LodBasen.Services.Interfaces;

namespace LodBasen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
        //    var builder = WebApplication.CreateBuilder(args);

        //    void ConfigureServices(IServiceCollection services)
        //    {
        //        //Add services to the container.
        //        builder.Services.AddRazorPages();
        //        builder.Services.AddDbContext<lodbasen_dk_db_lodbasenContext>();
        //        builder.Services.AddTransient<IBarnService, EFBarnService>();
        //        builder.Services.AddTransient<IGruppeService, EFGruppeService>();
        //}

        //var app = builder.Build();

        //    // Configure the HTTP request pipeline.
        //    if (!app.Environment.IsDevelopment())
        //    {
        //        app.UseExceptionHandler("/Error");
        //        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        //        app.UseHsts();
        //    }

        //    app.UseHttpsRedirection();
        //    app.UseStaticFiles();

        //    app.UseRouting();

        //    //app.UseAuthorization();

        //    app.MapRazorPages();

        //    app.Run();
        //}
    }
}