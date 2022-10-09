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
            // Adicionar a a conexão com o banco aos serviços de configuração
            // Recebe a string de conexão do arquivo appsettings.json
            services.AddDbContext<EduCarContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Azure")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            //options.UseSqlServer(Configuration.GetConnectionString("SqlServer")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            // Evita o erro de loop infinito em objetos relacionados
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EduCar", Version = "v1" });

                // Adiciona os comentários na documentação do Swagger
                var xmlArquivo = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlArquivo));
            });

            // Injeção de dependência do EduCarContext
            services.AddTransient<EduCarContext, EduCarContext>();

            // Injeção de dependência dos repositórios
            services.AddTransient<ICambioRepository, CambioRepository>();
            services.AddTransient<ICaracteristicasGeraisRepository, CaracteristicasGeraisRepository>();
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
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
