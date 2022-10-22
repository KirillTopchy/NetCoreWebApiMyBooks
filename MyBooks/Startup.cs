using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MyBooks.Data;
using MyBooks.Data.Services;
using MyBooks.Exceptions;

namespace MyBooks
{
    public class Startup
    {
        public string ConnectionString { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("DefaulConnectionString");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Configure DBContext with SQL
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConnectionString));

            // Configure the services
            services.AddTransient<BooksService>();
            services.AddTransient<AuthorsService>();
            services.AddTransient<PublishersService>();

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;

                //config.ApiVersionReader = new HeaderApiVersionReader("custom-version-header");
                //config.ApiVersionReader = new MediaTypeApiVersionReader();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyBooks", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyBooks v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // Exception Handling
            app.ConfigureBuildInExceptionHandler(loggerFactory); 
            //app.ConfigureCustomExceptionHandler(); if want to test remove comments and comment out previous line.

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Seed default data if DB is empty.
            //AppDbInitializer.Seed(app);
        }
    }
}
