using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Myapi.Middlewares;
using Swashbuckle.AspNetCore.Swagger;
using System;
using Newtonsoft.Json.Serialization;

namespace Myapi
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "MyAPI", Version = "v1" });
            });

            //去掉json名称转换。大写转小写
            services.AddMvc().AddJsonOptions(options =>
            {
                if (options.SerializerSettings.ContractResolver is DefaultContractResolver resolver)
                {
                    resolver.NamingStrategy = null;
                }
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAPI V1");
            });
            app.UseApiAuthorized(new ApiAuthorizedOptions
            {
                EncryptKey = Configuration.GetSection("ApiKey")["EncryptKey"],
                ExpiredSecond = Convert.ToInt32(Configuration.GetSection("ApiKey")["ExpiredSecond"])
            });
            app.UseStatusCodePages();
            app.UseMvc();
        }
    }
}
