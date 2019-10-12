using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeetingRoom.Models;
using MeetingRoom.Repository;
using MeetingRoom.Repository.Interface;
using MeetingRoom.Security;
using MeetingRoom.Service;
using MeetingRoom.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace MeetingRoom
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

            // Configurações CORS
            CorsPolicyBuilder corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
            });

            //injetando contexto do banco de dados
            services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("MeetingRoom")));

            //injetando classes
            services.AddScoped<ISalaService, SalaService>();
            services.AddScoped<ISalaRepository, SalaRepository>();

            services.AddScoped<IPessoaService, PessoaService>();
            services.AddScoped<IPessoaRepository, PessoaRepository>();

            services.AddScoped<IReservaService, ReservaService>();
            services.AddScoped<IReservaRepository, ReservaRepository>();

            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<ILoginService, LoginService>();

            //configurações para a utiulização de JWT
            var signinConfiguration = new SigninConfiguration();
            services.AddSingleton(signinConfiguration);//apenas uma instancia enquanto a aplicação estiver executando

            var configToken = new TokenConfiguration();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                    Configuration.GetSection("TokenConfigurations")//busca confiugurações do token em appsettings.json
                ).Configure(configToken);

            services.AddSingleton(configToken);

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(beareroptions =>
            {
                var parametrosValidacao = beareroptions.TokenValidationParameters;
                parametrosValidacao.IssuerSigningKey = signinConfiguration.Key;
                parametrosValidacao.ValidAudience = configToken.Audience;
                parametrosValidacao.ValidIssuer = configToken.Issuer;

                // Valida a assinatura de um token recebido
                parametrosValidacao.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                parametrosValidacao.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token (usado no caso de
                // de problemas de sincronização de tempo entre diferentes
                // computadores envolvidos no processo de comunicação)
                parametrosValidacao.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });


            // Configurando o serviço de documentação do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "API salas de reuniões",
                        Version = "v1",
                        Description = "Back end para gerenciamento dos agendamentos das salas de reuniões",
                        Contact = new Contact
                        {
                            Name = "Desenvolvedor: Willian Menezes dos Santos.",
                            Url = "https://www.linkedin.com/in/willian-menezes-9932b1b9/"
                        }
                    });

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("SiteCorsPolicy");

            app.UseHttpsRedirection();
            app.UseMvc();

            //Habilitando Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Versão 01");
            });

            //Iniciando a API na pagina do swagger
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);
        }
    }
}
