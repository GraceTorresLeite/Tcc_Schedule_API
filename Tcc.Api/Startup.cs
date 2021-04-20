using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Collections.Generic;
using Tcc.Api.Business;
using Tcc.Api.Business.Implementations;
using Tcc.Api.Hypermedia.Enricher;
using Tcc.Api.Hypermedia.Filters;
using Tcc.Api.Model.Context;
using Tcc.Api.Repository;
using Tcc.Api.Repository.Generic;


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

            //fazer verificação no log
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
                        Description = "Fundatec:Trabalho de Conclusão > \"Orientador: Prof.Guilherme Elias\" .",
                        Contact = new OpenApiContact
                        {
                            Name = "Grace Tôrres",
                            Url = new Uri("https://github.com/GraceTorresLeite/template-fundatec-tcc-ScheduleBeauty")
                        }
                    });
            });

            // ADICIONAR INJEÇÃO DE DEPENDÊNCIA - > depois ir no Personcontroller informar este serviço
            services.AddScoped<IScheduleFormsBusiness, ScheduleFormsBusinessImplementation>();
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
