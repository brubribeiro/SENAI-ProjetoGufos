using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

// Instalamos o Entity Framework
// dotnet tool install --global dotnet-ef

// Baixamos o pacote SQLServer do Entity Framework
// dotnet add package Microsoft.EntityFrameworkCore.SqlServer

// Baixamos o pacote que irá escrever nossos cógidos
// dotnet add package Microsoft.EntityFrameworkCore.Design

// Testamos se os pacotes foram instalados
// dotnet restore

// Testamos a instalação do EntityFramework(ef) - Se tiver certo aparece um unicórnio
// dotnet ef

// Código que criará o nosso Contexto da Base de Dados e nossos Models
// -o(Cria o diretório) -d(Cria das anotações no banco)
// dotnet ef dbcontext scaffold "Server=DESKTOP-JHOHBO4\SQLEXPRESS; Database=bdgufos; User Id=sa; Password=132" Microsoft.EntityFrameworkCore.SqlServer -o Models -d

namespace backend
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
            services.AddControllersWithViews().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
