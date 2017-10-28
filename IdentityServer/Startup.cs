using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IdentityServer4.Validation;
using IdentityServer.Service;
using IdentityServer4.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IdentityServer
{
    public class Startup
    {


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            const string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;database=IdentityServer4.Quickstart.EntityFramework-2.0.0;trusted_connection=yes;";
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddTestUsers(Config.GetUsers())
                // this adds the config data from DB (clients, resources)
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                });

            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddTransient<IProfileService, ProfileService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
           // InitializeDatabase(app);
            loggerFactory.AddConsole(LogLevel.Debug);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseIdentityServer();
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            // using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            // {
            //     serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

            //     var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            //     context.Database.Migrate();
            //     if (!context.Clients.Any())
            //     {
            //         foreach (var client in Config.GetClients())
            //         {
            //             context.Clients.Add(client.ToEntity());
            //         }
            //         context.SaveChanges();
            //     }

            //     if (!context.IdentityResources.Any())
            //     {
            //         foreach (var resource in Config.GetIdentityResources())
            //         {
            //             context.IdentityResources.Add(resource.ToEntity());
            //         }
            //         context.SaveChanges();
            //     }

            //     if (!context.ApiResources.Any())
            //     {
            //         foreach (var resource in Config.GetApiResources())
            //         {
            //             context.ApiResources.Add(resource.ToEntity());
            //         }
            //         context.SaveChanges();
            //     }
            // }
        }

    }
}
