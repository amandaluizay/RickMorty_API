using Azure.Storage.Blobs;
using Interview_API.Interface;
using Interview_API.Service;
using System.ComponentModel.DataAnnotations;

namespace Interview_API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IDeserializeService, DeserializeService>();

            var azureBlobStorageConnectionString = configuration.GetConnectionString("AzureBlobStorage");

            services.AddSingleton(x => new BlobServiceClient(azureBlobStorageConnectionString));
        }

    }


}
