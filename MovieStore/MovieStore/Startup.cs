using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using MovieStore.DataAccess;
using MovieStore.Middlewares;
using MovieStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MovieStore
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

            #region JWToken
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(opt => opt.TokenValidationParameters = new TokenValidationParameters
               {
                    //Tokeni kimler kullanabilir
                    ValidateAudience = true,
                    //Token daðýtýcýsýný kontrol et
                    ValidateIssuer = true,
                    //Token süresi dolduðunda geçersiz olsun
                    ValidateLifetime = true,
                    //tokeni kriptoladýðýmýz anahtar keyi kontrol et
                    ValidateIssuerSigningKey = true,
                   ValidIssuer = Configuration["Token:Issuer"],
                   ValidAudience = Configuration["Token:Audience"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"])),
                    //Sunucunun time zone u ile tokeni kullanan clientin zamanfarkýnda erken sonlanmamasý için zaman ekliyoruz
                    ClockSkew = TimeSpan.Zero
               });
            #endregion


            services.AddControllers().AddJsonOptions(option=>option.JsonSerializerOptions.ReferenceHandler=System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieStore", Version = "v1" });
            });

            services.AddDbContext<MovieContext>(options => options.UseInMemoryDatabase("MovieStoreDB"));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddSingleton<ILoggerService, ConsoleLogger>();
           
            services.AddScoped<IMovieContext>(provider=>provider.GetService<MovieContext>());

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieStore v1"));
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCustomExeptionMiddleware();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
