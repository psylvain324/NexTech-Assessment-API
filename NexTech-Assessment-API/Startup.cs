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
using NexTechAssessmentAPI.Data;
using NexTechAssessmentAPI.Controllers;
using NexTechAssessmentAPI.Interfaces;
using NexTechAssessmentAPI.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using NexTech_Assessment_API.Interfaces;
using NexTech_Assessment_API.Services;
using NexTech_Assessment_API.Controllers;

namespace NexTechAssessmentAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase(databaseName: "NexTechAssessmentDB"));

            services.TryAddTransient(s =>
            {
                return s.GetRequiredService<IHttpClientFactory>().CreateClient(string.Empty);
            });

            services.AddCors(options =>
            {
                options.AddPolicy(
                  "CorsPolicy",
                  builder => builder.WithOrigins("https://localhost:4200")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowAnyOrigin());
            });

            services.AddControllers();
            services.AddMvc();
            services.AddResponseCaching();

            services.AddTransient<IStoryService, StoryService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddHttpClient<StoryController>();
            services.AddHttpClient<CommentController>();

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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "NexTech Assessment - Phillip Sylvain V1");
            });

            app.UseRouting();
            app.UseAuthorization();
            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseResponseCaching();
            app.UseHealthChecks("/health", new HealthCheckOptions { ResponseWriter = JsonResponseWriter });

        }

        private Task JsonResponseWriter(HttpContext context, HealthReport report)
        {
            context.Response.ContentType = "application/json";
            return JsonSerializer.SerializeAsync(context.Response.Body, new { Status = report.Status.ToString() },
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
    }
}