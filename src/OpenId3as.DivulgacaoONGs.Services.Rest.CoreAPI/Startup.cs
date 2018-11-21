using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Middlewares;
using Swashbuckle.AspNetCore.Swagger;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDependencyInjections(Configuration);
            services.AddSwaggerGen(c =>
            {
                c.DescribeAllEnumsAsStrings();
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Divulgação de ONG's",
                        Version = "v1",
                        Description = "API Rest desenvolvida em ASPNET Core 2.2",
                        Contact = new Contact
                        {
                            Name = "Marcelo Ribeiro de Albuquerque",
                            Url = "https://github.com/openid3as"
                        }
                    });

                //string caminhoAplicacao = PlatformServices.Default.Application.ApplicationBasePath;
                //string nomeAplicacao = PlatformServices.Default.Application.ApplicationName;
                //string caminhoXmlDoc = Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");
                //c.IncludeXmlComments(caminhoXmlDoc);
            });
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
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Divulgação ONG's");
            });
        }
    }
}
