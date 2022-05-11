using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ProjectEcommerce.src.data;
using ProjectEcommerce.src.repositories;
using ProjectEcommerce.src.repositories.implements;
using ProjectEcommerce.src.services;
using ProjectEcommerce.src.services.implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEcommerce
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
            services.AddDbContext<ProjectEcommerceContext>(opt => opt.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            // Repositories
            services.AddScoped<IUser, UserImplements>();
            services.AddScoped<IProduct, ProductImplements>();
            services.AddScoped<IPurchase, PurchaseImplements>();

            // Controllers
            services.AddCors();
            services.AddControllers();


            // Configuração de Serviços

            services.AddScoped<IAuthentication, AuthenticationServices>();

            // Configuração do Token Autenticação JWTBearer

            var key = Encoding.ASCII.GetBytes(Configuration["Settings:Secret"]);
            services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(b =>
            {
                 b.RequireHttpsMetadata = false;
                 b.SaveToken = true;
                b.TokenValidationParameters = new TokenValidationParameters
             {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(key),
                       ValidateIssuer = false,
                    ValidateAudience = false
                };
}           );
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ProjectEcommerceContext context)
        {
            if (env.IsDevelopment())
            {
                context.Database.EnsureCreated();
                app.UseDeveloperExceptionPage();
            }

            // Ambiente de produção
            // Rotas
            app.UseRouting();
            app.UseCors(c => c
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            // Autenticação e Autorização
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
