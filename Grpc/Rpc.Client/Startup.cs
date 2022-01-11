using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rpc.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static Rpc.Server.Greeter;

namespace Rpc.Client
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpcClient<GreeterClient>(options => {
                options.Address = new Uri("https://localhost:5001");
            })
            .ConfigurePrimaryHttpMessageHandler(provider =>
            {
                var handler = new SocketsHttpHandler();
                handler.SslOptions.RemoteCertificateValidationCallback = (a, b, c, d) => true; //允许无效、或自签名证书
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

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    GreeterClient service = context.RequestServices.GetService<GreeterClient>();

                    try
                    {
                        var r = service.SayHello(new HelloRequest { Name = "Mike" });

                        await context.Response.WriteAsync(r.Message);
                    }
                    catch (Exception ex)
                    {
                    }
                });
            });
        }
    }
}
