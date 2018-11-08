using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using webServiceNet.Models;
using webServiceNet.Repository;

namespace webServiceNet
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            // Remover comentario abaixo caso realize configuracao para acessar um banco de 
            //dados externo. E adicionar no arquivo "appsettings.json" na chave "DefaultConnection"
            // a url do seu banco.

            // services.AddDbContext<CervejaDbContext>(options => 
            // options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            //Foi utilizado salvar os dados em memoria
            //Caso utilize um banco externo, comente a linha abaixo.
            services.AddDbContext<CervejaDbContext>(opt => opt.UseInMemoryDatabase("WEBAPI"));
            
            
            services.AddTransient<ICervejaRepository, CervejaRepository>();

            //especifica o esquema usado para autenticacao do tipo Bearer e
            //define configuracoes como chave, algoritmo, validade, data de expiracao...
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters{
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "robson.net",
                    ValidAudience = "robson.net",
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["SecurityKey"]))
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Token invalido... : "+context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token valido... : "+context.SecurityToken);
                        return Task.CompletedTask;
                    } 
                };
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //habilita o uso da autenticacao
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
