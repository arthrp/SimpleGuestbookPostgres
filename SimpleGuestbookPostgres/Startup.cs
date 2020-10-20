using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SimpleGuestbookPostgres.Dto;
using SimpleGuestbookPostgres.Repositories;
using static Dapper.SqlMapper;

namespace SimpleGuestbookPostgres
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
            services.AddControllersWithViews();
            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));
            services.AddSingleton<AppSettings>(sp => sp.GetRequiredService<IOptions<AppSettings>>().Value);
            services.AddSingleton<IPostsRepository, PostsRepository>();

            SqlMapper.SetTypeMap(typeof(GuestbookPostDto), GetPostsTypeMapper());

            new DbInitializer("Server=127.0.0.1;Port=5432;Userid=root;Password=supersecretpw;Timeout=15;SslMode=Disable;Database=guestbookdb").Init();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private ITypeMap GetPostsTypeMapper()
        {
            var columnMap = new Dictionary<string, string>()
            {
                { "Id", "id" }
            };

            var columnMapper = new Func<Type, string, PropertyInfo>((type, columnName) =>
            {
                if (columnMap.ContainsKey(columnName))
                    return type.GetProperty(columnMap[columnName]);

                return type.GetProperty(columnName);
            });

            return new CustomPropertyTypeMap(typeof(GuestbookPostDto), (type, columnName) => columnMapper(type, columnName));
        }
    }
}
