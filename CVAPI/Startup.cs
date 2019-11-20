using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVCore.Entities;
using CVInfrastructure.Data;
using Microsoft.AspNetCore.Identity;
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
using AutoMapper;
using CVCore.Interfaces;
using CVCore.Services;
using CVInfrastructure.Extensions;

namespace CVAPI
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
            services.AddAutoMapper(typeof(Startup));
            services.AddCors();
            services.AddHealthChecks();

            //services.AddDbContext<CVStaffDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddDbContext<CVStaffDBContext>(options => options.UseInMemoryDatabase(databaseName: "CVStaff"));
            
            services.AddScoped<IJwtToken, JwtToken>();
            services.AddScoped<IRepository, CVStaffDBContext>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ISPNationService, SPNationService>();
            services.AddScoped<ISPEducationService, SPEducationService>();
            services.AddScoped<IStaffService, StaffService>();            
            
            // ===== Add Jwt Authentication ========
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            
            services.AddIdentity<ApplicationUser, IdentityRole>(
                      option =>
                      {
                          option.Password.RequireDigit = false;
                          option.Password.RequiredLength = 6;
                          option.Password.RequireNonAlphanumeric = false;
                          option.Password.RequireUppercase = false;
                          option.Password.RequireLowercase = false;
                      }
                  ).AddEntityFrameworkStores<CVStaffDBContext>()
                  .AddDefaultTokenProviders();

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,                                        
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                };
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(builder => builder                
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );
            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
