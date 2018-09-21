using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Raven.Embedded;
using static System.Environment;

namespace RentMyTools.Api
{
    public class Startup
    {
        private const string DBName = "RentMyTools";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            RegisterDatabaseServices(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        private void RegisterDatabaseServices(IServiceCollection services)
        {
            services.AddSingleton<EmbeddedServer>(InitializeDBServer);
            services.AddSingleton<IDocumentStore>(provider => provider.GetService<EmbeddedServer>()
                .GetDocumentStore(DBName)
                .Initialize());
            services.AddScoped<IDocumentSession>(provider => provider.GetService<IDocumentStore>().OpenSession());
        }

        private EmbeddedServer InitializeDBServer(IServiceProvider provider)
        {
            var config = new RavenDBConfigurationOptions();
            Configuration.GetSection("RavenDB").Bind(config);
            EmbeddedServer.Instance.StartServer(new ServerOptions{
                DataDirectory = Path.GetFullPath(config.DataDirectory),
                ServerUrl = $"{config.ServerAddress}:{config.ServerPort}"
            });

            return EmbeddedServer.Instance;
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
