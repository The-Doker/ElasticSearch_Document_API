using ElasticSearch_Document_API.Services;
using ElasticSearch_Document_API.Services.Abstraction;
using ElasticSearch_Document_API.Services.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace ElasticSearch_Document_API
{
    public class Startup
    {
        private bool isSwaggerEnabled = false;
        const string UNENCRYPTED_SWITCH_NAME = "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            if (isSwaggerEnabled)
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ElasticSearch_Document_API", Version = "v1" });
                });
            
            AppContext.SetSwitch(UNENCRYPTED_SWITCH_NAME, true);
            services.AddTransient<IDocumentSaver, gRpcDocumentSaver>();
            services.AddTransient<IDocumentSearcher, gRpcDocumentSearcher>();
            services.AddTransient<IDocumentGiver, gRpcDocumentGiver>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                if (isSwaggerEnabled)
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ElasticSearch_Document_API v1"));
                }
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
