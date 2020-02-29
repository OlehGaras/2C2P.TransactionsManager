using System;
using _2C2P.TransactionsManager.Domain.Service.Abstractions;
using _2C2P.TransactionsManager.Domain.Service.Implementations;
using _2C2P.TransactionsManager.Infrastructure;
using _2C2P.TransactionsManager.Infrastructure.Csv;
using _2C2P.TransactionsManager.Infrastructure.Xml;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace _2C2P.TransactionsManager.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddLogging(builder => { builder.AddConsole(); });

            services.AddScoped<IXMlTransactionFileValidator, XmlTransactionFileValidator>();
            services.AddScoped<IFileParser, XmlFileParser>();
            services.AddScoped<IFileParser, CsvFileParser>();
            services.AddScoped<IUploadService, UploadService>();
            services.AddScoped<ITransactionsService, TransactionsService>();
            services.AddScoped<IFileParseStrategy, FileParseStrategy>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                    builder.Run(async context =>
                        {
                            context.Response.StatusCode = 500;
                            await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                        }
                    ));
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=UploadTransactionFile}/{action=Upload}");
            });
        }
    }
}
