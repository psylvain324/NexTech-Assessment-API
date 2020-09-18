using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NexTechAssessmentAPI.IntegrationTests
{
    public class TestFixture<TStartup> : IDisposable
    {
        public static string GetProjectPath(string projectRelativePath, Assembly startupAssembly)
        {
            string projectName = startupAssembly.GetName().Name;
            string applicationBasePath = AppContext.BaseDirectory;
            DirectoryInfo directoryInfo = new DirectoryInfo(applicationBasePath);

            do
            {
                directoryInfo = directoryInfo.Parent;

                DirectoryInfo projectDirectoryInfo = new DirectoryInfo(Path.Combine(directoryInfo.FullName, projectRelativePath));

                if (projectDirectoryInfo.Exists)
                {
                    if (new FileInfo(Path.Combine(projectDirectoryInfo.FullName, projectName, $"{projectName}.csproj")).Exists)
                    {
                        return Path.Combine(projectDirectoryInfo.FullName, projectName);
                    }
                }

            }
            while (directoryInfo.Parent != null);
            throw new Exception($"Project root could not be located using the application root {applicationBasePath}.");
        }

        private readonly TestServer Server;

        public TestFixture()
            : this(Path.Combine(""))
        {
        }

        public HttpClient Client { get; }

        public void Dispose()
        {
            Client.Dispose();
            Server.Dispose();
        }

        protected virtual void InitializeServices(IServiceCollection services)
        {
            Assembly startupAssembly = typeof(TStartup).GetTypeInfo().Assembly;

            ApplicationPartManager manager = new ApplicationPartManager
            {
                ApplicationParts =
                {
                    new AssemblyPart(startupAssembly)
                },
                FeatureProviders =
                {
                    new ControllerFeatureProvider(),
                    new ViewComponentFeatureProvider()
                }
            };

            services.AddSingleton(manager);
        }

        protected TestFixture(string relativeTargetProjectParentDir)
        {
            Assembly startupAssembly = typeof(TStartup).GetTypeInfo().Assembly;
            string contentRoot = GetProjectPath(relativeTargetProjectParentDir, startupAssembly);

            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(contentRoot)
                .AddJsonFile("appsettings.json");

            IWebHostBuilder webHostBuilder = new WebHostBuilder()
                .UseContentRoot(contentRoot)
                .ConfigureServices(InitializeServices)
                .UseConfiguration(configurationBuilder.Build())
                .UseEnvironment("Development")
                .UseStartup(typeof(TStartup));

            Server = new TestServer(webHostBuilder);
            Client = Server.CreateClient();
            Client.BaseAddress = new Uri("http://localhost:5001");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}