using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Repository.dbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Logging;
using BusinessLogic.Models;
using BusinessLogic;
using Repository;

namespace BeauCrumley_p1
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
            services.AddDbContext<Project1_DBContext>( options => 
            {
                options.UseSqlServer("Server=.\\SQLEXPRESS;Database=Project1_DB;Trusted_Connection=True;");
            });

            services.AddScoped<IProcessLogger, ConsoleLogger>();
            services.AddScoped<IUserMethods, UserMethods>();
            services.AddScoped<ILoginMethods, LoginMethods>();
            services.AddScoped<IFactory, Factory>();
            services.AddScoped<IDataFetcher, DataFetcher>();
            services.AddScoped<IDataSaver, DataSaver>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BeauCrumley_p1", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BeauCrumley_p1 v1"));
            }

            app.UseStatusCodePages();

            app.UseHttpsRedirection();

            app.UseRewriter(new RewriteOptions()
                .AddRedirect("^$", "index.html"));

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
