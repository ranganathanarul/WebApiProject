using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiSwagger.Middleware;

namespace WebApiSwagger
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
            //Dependency Injection to register class
            services.AddTransient<ExceptionMiddleware>();
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "First API", Version = "Version 1.0.0" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI( api =>
                {
                    api.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API");
                    
                });
            }

            //Map() method is used to map the middleware to a specific URL
            app.Map("/ErrorValues", (app) => { });
            app.Map("/Error", MapHandler);
            app.UseMiddleware<ExceptionMiddleware>();

            app.MapWhen(context => context.Request.Query.ContainsKey("Err"), HandleRequestWithQuery);

            //app.Use(async (context, next) => {
            //    Console.WriteLine("Before Request Placed");

            //    await next();

            //    Console.WriteLine("After Request Placed");

            //});

            //app.Run(async context => {
            //    Console.WriteLine("Showing the Middleware.");
            //    await context.Response.WriteAsync("Hello Ranganathan, Welcome to the middleware.");
            //});

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void HandleRequestWithQuery(IApplicationBuilder objapp)
        {
            //Use() method is used to insert a new middleware in the pipeline
            objapp.Use(async (context,next) => {
                Console.WriteLine("Error Message contains");
                //next () method is used to pass the execution to the next middleware
                await next();
            });
        }

        private void MapHandler(IApplicationBuilder obj)
        {
            //Run() method is used to complete the middleware execution.
            obj.Run(async context => {
                Console.WriteLine("Error Method");
                await context.Response.WriteAsync("Error Method");
            });
        }
    }
}
