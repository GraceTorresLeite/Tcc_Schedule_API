using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using Tcc.Api.Business;
using Tcc.Api.Business.Implementations;
using Tcc.Api.Configurations;
using Tcc.Api.Hypermedia.Enricher;
using Tcc.Api.Hypermedia.Filters;
using Tcc.Api.Model.Context;
using Tcc.Api.Repository;
using Tcc.Api.Repository.Generic;
using Tcc.Api.Services;
using Tcc.Api.Services.Implementations;

namespace Tcc.Api
{
    public class Startup
    {

        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;

            Environment = environment;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //setar valores do appSettings
            var tokenConfigurations = new TokenConfiguration();

            // armazena os valores na classe
            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                Configuration.GetSection("TokenConfigurations")
                )
                .Configure(tokenConfigurations);

            //injetando no servi�o
            services.AddSingleton(tokenConfigurations);

            // parametros de autentica��o
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options => 
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = tokenConfigurations.Issuer,
                        ValidAudience = tokenConfigurations.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
                    };
                });

            // add autoriza��o
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build());
            });

            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            services.AddControllers();
           
            var connection = Configuration["MySQLConnection:MySQLConnectionString"]; // acessa o arquivo appsetings
            //ADD DBCOntext
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connection));

            //fazer verifica��o no log
            if (Environment.IsDevelopment())
            {
                MigrationDatabase(connection);
            }

            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));

            })
                .AddXmlSerializerFormatters();

            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ContentResponseEnricherList.Add(new ScheduleFormEnricher());

            services.AddSingleton(filterOptions);

            //Adicionando versionamento API
            services.AddApiVersioning();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Schedule Beauty",
                        Version = "v1",
                        Description = "Fundatec:Trabalho de Conclus�o > \"Orientador: Prof.Guilherme Elias\" .",
                        Contact = new OpenApiContact
                        {
                            Name = "Grace T�rres",
                            Url = new Uri("https://github.com/GraceTorresLeite/template-fundatec-tcc-ScheduleBeauty")
                        }
                    });
            });

            // ADICIONAR INJE��O DE DEPEND�NCIA - > depois ir no Personcontroller informar este servi�o
            services.AddScoped<IScheduleFormsBusiness, ScheduleFormsBusinessImplementation>();
            services.AddScoped<ILoginBusiness, LoginBusinessImplementation>();

            services.AddTransient<ITokenService,TokenService>();

            services.AddScoped<IUserRepository, UserRepository>();

            // services.AddScoped<IScheduleFormRepository, ScheduleFormRepositoryImplementation>();
            services.AddScoped(typeof(IRepository<>),typeof(GenericRepositoryImplementation<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseSwagger();

            app.UseSwaggerUI(c=> {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Schedule Beauty - v1");
            });
            var option = new RewriteOptions();
            option.AddRedirect("^$","swagger");
            app.UseRewriter(option);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("DefaultApi","{controller=values}/{id?}");
            });
        }

        private void MigrationDatabase(string connection)
        {
            try
            {
                var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connection);
                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "db/migrations", "db/dataset" }, // quais diretorios (dois diretorios)
                    IsEraseDisabled = true,
                };
                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Database migration failed", ex);
                throw;
            }
        }
    }
}
