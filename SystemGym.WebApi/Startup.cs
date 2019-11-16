using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using SystemGym.DataAccess.Models;
using SystemGym.Service;

namespace SystemGym.WebApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContextPool<SystemGymContext>(options => options.UseSqlServer(this.Configuration.GetConnectionString("SystemGymContext")));
            services.AddMvc();
            services.AddCors();
            services.AddScoped<PessoaService>();
            services.AddScoped<UsuarioService>();
            services.AddScoped<AlunoService>();
            services.AddScoped<ColaboradorService>();
            services.AddScoped<VisitanteService>();
            services.AddScoped<DashboardService>();
            services.AddScoped<AddressService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "SystemGym API v1",
                    Description = "RESTful API desenvolvida em ASP.NET Core 2.2 Web API",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Guilherme", Email = "guilherme.santana@sempreceub.com", Url = "http://www.uniceub.com.br" },
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(builder =>
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader());

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "API v1");
                c.DefaultModelsExpandDepth(0);
                c.EnableFilter();
                c.DisplayRequestDuration();
            });
        }
    }
}
