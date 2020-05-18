using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EmployeeManagement.web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using EmployeeManagement.web.Models;

namespace EmployeeManagement.web
{
    public class Startup
    {
        private readonly IWebHostEnvironment hostingEnvironment;

        public Startup(IConfiguration configuration,IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddAutoMapper(typeof(EmployeeProfile));
            
            services.AddHttpClient<IDepatmentService, DepatmentService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44386/");
            }).ConfigurePrimaryHttpMessageHandler(() => {
                var handler = new HttpClientHandler();
                if (hostingEnvironment.IsDevelopment())
                {
                    handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                }
                return handler;
            });
            services.AddHttpClient<IEmployeeService,EmployeeService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44386/");
            }).ConfigurePrimaryHttpMessageHandler(() => {
                var handler = new HttpClientHandler();
                if (hostingEnvironment.IsDevelopment())
                {
                    handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                }
                return handler;
            }); 
            
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

           //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
