using LodBasen.Models;
using LodBasen.Services.EFServices;
using LodBasen.Services.Interfaces;

namespace LodBasen
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddDistributedMemoryCache();

            services.AddSession(options => 
            { 
                options.IdleTimeout = TimeSpan.FromSeconds(36000);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential= true;
            });
            services.AddDbContext<lodbasen_dk_db_lodbasenContext>();
            services.AddTransient<IBarnService, EFBarnService>();
            services.AddTransient<IGruppeService, EFGruppeService>();
            services.AddTransient<ILederService, EFLederService>();
            services.AddTransient<ISælgerService, EFSælgerService>();
            services.AddTransient<IModtagerService, EFModtagerService>();   
            services.AddTransient<ISalgService, EFSalgService>();   
            services.AddTransient<ILodseddelService, EFLodseddelService>();
            services.AddTransient<IAdminService, EFAdminService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
