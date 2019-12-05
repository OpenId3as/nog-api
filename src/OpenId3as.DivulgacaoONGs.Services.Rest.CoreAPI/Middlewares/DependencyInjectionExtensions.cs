using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OpenId3as.DivulgacaoONGs.Application.AutoMapper.Config;
using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.IoC;
using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Context;
using StackExchange.Redis;
using System;
using AM = AutoMapper;
using Mongo = OpenId3as.DivulgacaoONGs.Infra.Data.Context.Mongo;
using Postgres = OpenId3as.DivulgacaoONGs.Infra.Data.Context.Postgres;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Middlewares
{
    public static class DependencyInjectionExtensions
    {
        public static void AddDependencyInjections(this IServiceCollection services, IConfiguration configuration)
        {
            // AutoMapper
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AM.IConfigurationProvider>(), sp.GetService));
            AutoMapperConfig.RegisterMappingsInit();
            services.AddSingleton(Mapper.Configuration);

            // Swagger
            _ = services.AddSwaggerGen(c =>
              {
                  c.SwaggerDoc(
                      "v1",
                      new OpenApiInfo
                      {
                          Title = "Divulgação de ONG's",
                          Version = "v1",
                          Description = "API Rest desenvolvida em ASPNET Core 2.2",
                          Contact = new OpenApiContact
                          {
                              Name = "Marcelo Ribeiro de Albuquerque",
                              Url = new Uri("https://github.com/openid3as")
                          }
                      });
                  c.CustomSchemaIds(x => x.FullName);
              });

            // Postgres
            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<Postgres.NOGContext>(
                    options =>
                        options.UseNpgsql(configuration.GetConnectionString("ST_POSTGRES_NOG")))
                .BuildServiceProvider();

            // Log
            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<LogContext>(
                    options =>
                        options.UseNpgsql(configuration.GetConnectionString("ST_POSTGRES_LOG")))
                .BuildServiceProvider();

            // Mongo
            var mongoContext = new Mongo.NOGContext(
                configuration.GetConnectionString("ST_MONGO_NOG"),
                configuration.GetSection("ConnectionExtraInfo").GetSection("O_MONGO").GetValue<string>("ST_DATABASE_NOG"));
            services.AddSingleton<Mongo.NOGContext>(mongoContext);

            // Redis
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("ST_REDIS_NOG");
                options.InstanceName = configuration.GetSection("ConnectionExtraInfo").GetSection("O_REDIS").GetValue<string>("ST_INSTANCE_NAME");
            });

            var redis = ConnectionMultiplexer.Connect(
                    $"{configuration.GetConnectionString("ST_REDIS_NOG")}, abortConnect=false, syncTimeout=10000");
            services.AddDataProtection().PersistKeysToRedis(redis, "key-nog-api");

            // HyperMedia
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(x =>
            {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });

            // Dependency Injections
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
