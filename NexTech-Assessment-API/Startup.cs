using System;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NexTech_Assessment_API.Data;
using TechAssessment.Controllers;
using TechAssessment.Interfaces;
using TechAssessment.Services;

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

            services.AddHttpClient<StoryController>();

            services.AddControllers();
            services.AddMvc();
            services.AddTransient<IStoryService, StoryService>();

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = "NextTechAssessment";
                options.InstanceName = "NextTechAssessmentInstance";
            });
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
