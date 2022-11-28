using System.Reflection;
using FigureGeneration.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DiscosGroove.Main.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddServices(this IServiceCollection services)
	{
		services.RegisterImplementationsOf<IPlotGenerator>(new[] {typeof(Program).Assembly});
		services.AddTransient<PlotGeneratorService>();
		return services;
	}
	
	private static void RegisterImplementationsOf<T>(this IServiceCollection services, Assembly[] assemblies, ServiceLifetime lifetime = ServiceLifetime.Transient)
	{
		IEnumerable<TypeInfo> typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T))));
		foreach (TypeInfo type in typesFromAssemblies)
		{
			services.Add(new(typeof(T), type, lifetime));
		}
	}
}
