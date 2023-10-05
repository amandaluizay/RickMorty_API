using Interview_API.Interface;
using Interview_API.Service;
using System.ComponentModel.DataAnnotations;

namespace Interview_API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IDeserializeService, DeserializeService>();
        }
    }
}
