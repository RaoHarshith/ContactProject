using System.Reflection;
using ContactApi.BLL;
using ContactApi.Core;
using ContactApi.Interfaces;
using ContactApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace ContactApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
			Configuration.Bind(Global.ConnectionString);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
			services.AddDbContext<Context>(opt => opt.UseMySql(Global.ConnectionString));
			services.AddScoped<IContactManager, ContactManager>();
			services.AddSwaggerGen(c =>
			{
				var assemblyName = Assembly.GetExecutingAssembly().GetName();
				c.SwaggerDoc(assemblyName.Version.ToString(), new Info
				{
					Title = assemblyName.Name,
					Version = assemblyName.Version.ToString()
				});
			});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
				app.UseSwagger();

				var assemblyName = Assembly.GetExecutingAssembly().GetName();
				app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint($"/swagger/{assemblyName.Version.ToString()}/swagger.json", assemblyName.Name);
				});
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
