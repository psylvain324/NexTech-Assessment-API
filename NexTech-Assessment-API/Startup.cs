using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using NexTech_Assessment_API.Data;
using TechAssessment.Controllers;
using TechAssessment.Interfaces;
using TechAssessment.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace NexTech_Assessment_API
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
            services.AddSwaggerGen();
            services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase(databaseName: "NexTechAssessmentDB"));

            services.AddHttpClient("hackerNoonClient", client =>
            {
                client.BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/");
            });

            services.TryAddTransient(s =>
            {
                return s.GetRequiredService<IHttpClientFactory>().CreateClient(string.Empty);
            });

            services.AddControllers();
            services.AddMvc();
            services.AddResponseCaching();

            services.AddTransient<IStoryService, StoryService>();
            services.AddHttpClient<StoryController>();

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = "NextTechAssessment";
                options.InstanceName = "NextTechAssessmentInstance";
            });

            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseResponseCaching();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHealthChecks("/health", new HealthCheckOptions { ResponseWriter = JsonResponseWriter });

        }

        private async Task JsonResponseWriter(HttpContext context, HealthReport report)
        {
            context.Response.ContentType = "application/json";
            await JsonSerializer.SerializeAsync(context.Response.Body, new { Status = report.Status.ToString() },
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
    }
}