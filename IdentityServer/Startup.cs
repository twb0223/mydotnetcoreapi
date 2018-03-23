using IdentityServer.Service;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace IdentityServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                 .AddInMemoryApiResources(Config.GetApiResources())
                 .AddInMemoryClients(Config.GetClients());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Debug);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            //using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            //{
            //    serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
            //    var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            //    context.Database.Migrate();
            //    if (!context.Clients.Any())
            //    {
            //        foreach (var client in Config.GetClients())
            //        {
            //            context.Clients.Add(client.ToEntity());
            //        }
            //        context.SaveChanges();
            //    }
            //    if (!context.IdentityResources.Any())
            //    {
            //        foreach (var resource in Config.GetIdentityResources())
            //        {
            //            context.IdentityResources.Add(resource.ToEntity());
            //        }
            //        context.SaveChanges();
            //    }
            //    if (!context.ApiResources.Any())
            //    {
            //        foreach (var resource in Config.GetApiResources())
            //        {
            //            context.ApiResources.Add(resource.ToEntity());
            //        }
            //        context.SaveChanges();
            //    }
            //}
        }

    }
}
