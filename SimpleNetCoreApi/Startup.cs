using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleNetCoreApi.Infrastructure;
using Swashbuckle.AspNetCore.Swagger;

namespace SimpleNetCoreApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly string _name;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _name = typeof(Startup).Assembly.GetName().Name;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddAutoMapper();

            services.AddDbContext<DefaultContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(nameof(DefaultContext))));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = _name, Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", _name);
            });
        }
    }
}
