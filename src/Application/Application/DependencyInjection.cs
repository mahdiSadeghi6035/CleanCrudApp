using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class DependencyInjection
{
    public static void RegisterApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfj => cfj.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
    }
}
