using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenId3as.DivulgacaoONGs.Application.AutoMapper.Config;
using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.IoC;
using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Context;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.Swagger;
using AM = AutoMapper;
using Mongo = OpenId3as.DivulgacaoONGs.Infra.Data.Context.Mongo;
using Postgres = OpenId3as.DivulgacaoONGs.Infra.Data.Context.Postgres;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Middlewares
{
    public static class DependencyInjectionExtensions
    {
        public static void AddDependencyInjections(this IServiceCollection services, IConfiguration configuration)
        {
            //AutoMapper
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AM.IConfigurationProvider>(), sp.GetService));
            AutoMapperConfig.RegisterMappingsInit();
            services.AddSingleton(Mapper.Configuration);

            //Swagger
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

            //Postgres
            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<Postgres.NOGContext>(
                    options =>
                        options.UseNpgsql(configuration.GetConnectionString("ST_POSTGRES_NOG"))
                    )
                .BuildServiceProvider();

            //Log
            //services
            //    .AddDbContext<LogContext>(
            //        options =>
            //            options.UseNpgsql(configuration.GetConnectionString("ST_POSTGRES_LOG"))
            //        );

            //Mongo
            var mongoContext = new Mongo.NOGContext(
                configuration.GetConnectionString("ST_MONGO_NOG"),
                configuration.GetSection("ConnectionExtraInfo").GetSection("O_MONGO").GetValue<string>("ST_DATABASE_NOG")
                );
            services.AddSingleton<Mongo.NOGContext>(mongoContext);

            //Redis
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("ST_REDIS_NOG");
                options.InstanceName = configuration.GetSection("ConnectionExtraInfo").GetSection("O_REDIS").GetValue<string>("ST_INSTANCE_NAME");
            });

            var redis = ConnectionMultiplexer.Connect(
                    $"{configuration.GetConnectionString("ST_REDIS_NOG")}, abortConnect=false, syncTimeout=10000"
                );
            services.AddDataProtection().PersistKeysToRedis(redis, "key-nog-api");

            //Dependency Injections
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
