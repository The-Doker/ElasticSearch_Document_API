using ElasticSearch_Document_API.Middlewares;
using ElasticSearch_Document_API.Services;
using ElasticSearch_Document_API.Services.Abstraction;
using ElasticSearch_Document_API.Services.Implementation;
using ElasticSearch_gRPC_Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.ServiceModel;

namespace ElasticSearch_Document_API
{
    public class Startup
    {
        private bool isSwaggerEnabled = false;
        private const string UNENCRYPTED_SWITCH_NAME = "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport";
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
            services.AddGrpcClient<DocumentHelper.DocumentHelperClient>(_ =>
            {
                _.Address = new Uri(Configuration["Enviroments:GRPC_ADDRESS"]);
            });
            services.AddTransient<IDocumentSaver, gRpcDocumentSaver>();
            services.AddTransient<IDocumentSearcher, gRpcDocumentSearcher>();
            services.AddTransient<IDocumentGiver, gRpcDocumentGiver>();
            services.AddTransient( _ =>
            {
                var binding = new WSHttpBinding()
                {
                    Name = "WcfTypesService"
                };
                binding.Security.Mode = SecurityMode.TransportWithMessageCredential;
                var endpoint = new EndpointAddress(Configuration["Enviroments:GETTYPES_ADDRESS"]);
                
                return new GetDataService.GetDataServiceClient(binding, endpoint);
            }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
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
