using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EduCar.Contexts;
using EduCar.Interfaces;
using EduCar.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace EduCar
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
            // Adicionar a a conex�o com o banco aos servi�os de configura��o
            // Recebe a string de conex�o do arquivo appsettings.json
            services.AddDbContext<EduCarContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Azure")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            //options.UseSqlServer(Configuration.GetConnectionString("AzureNew")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            //options.UseSqlServer(Configuration.GetConnectionString("SqlServer")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            //options.UseSqlServer(Configuration.GetConnectionString("SqlVMAzure")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            // Evita o erro de loop infinito em objetos relacionados
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "EduCar",
                    Version = "v1",
                    Description = "Case final - EduCar - Servi�o Online de com�rcio de ve�culos",
                    Contact = new OpenApiContact()
                    {
                        Name = "Reposit�rio do c�digo completo",
                        Url = new Uri("https://github.com/sthevanbello/EduCar")
                    }
                });

                // Cria��o do bot�o 'Authorize' no Swagger, para colar o token no Bearer e verific�-lo
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = @"Enviar o token para autentica��o com o formato de exemplo 'Bearer abcd1234'",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new string[] {}
                    }
                });

                // Adicionar configura��es extras da documenta��o, para ler os XMLs
                //Combinar informa��es, gerando o Assembly
                var xmlArquivo = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlArquivo));

            });


            // Configura��o do JWT para a autentica��o de token
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
            .AddJwtBearer("JwtBearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("case-chave-autenticacao")),
                    ClockSkew = TimeSpan.FromMinutes(30),
                    ValidIssuer = "case.webAPI",
                    ValidAudience = "case.webAPI"
                };
            });


            // Inje��o de depend�ncia do EduCarContext
            services.AddTransient<EduCarContext, EduCarContext>();

            // Inje��o de depend�ncia dos reposit�rios
            services.AddTransient<ICambioRepository, CambioRepository>();
            services.AddTransient<ICartaoRepository, CartaoRepository>();
            services.AddTransient<IConcessionariaRepository, ConcessionariaRepository>();
            services.AddTransient<IDirecaoRepository, DirecaoRepository>();
            services.AddTransient<IEnderecoRepository, EnderecoRepository>();
            services.AddTransient<IFichaTecnicaRepository, FichaTecnicaRepository>();
            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<IPedidoRepository, PedidoRepository>();
            services.AddTransient<ITipoUsuarioRepository, TipoUsuarioRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IVeiculoRepository, VeiculoRepository>();
            services.AddTransient<IStatusVendaRepository, StatusVendaRepository>();
            services.AddTransient<ICaracteristicasGeraisRepository, CaracteristicasGeraisRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EduCar v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
