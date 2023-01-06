using System.Reflection;
using FigureGeneration.Data;
using FigureGeneration.Options;
using FigureGeneration.PlotGeneration;
using FigureGeneration.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace FigureGeneration.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddServices(this IServiceCollection services, IConfigurationRoot configurationRoot)
	{
		Log.Logger = new LoggerConfiguration()
			.ReadFrom.Configuration(configurationRoot)
			.CreateLogger();
		
		services.AddLogging(lb => lb.AddSerilog());
		services.RegisterImplementationsOf<IPlotGenerator>(new[] {typeof(Program).Assembly});
		services.AddSingleton<ArchivedDataRepository>();
		services.AddSingleton<FigureGenerationApplication>();
		services.AddTransient<DataFileService>();

		services.Configure<AppSettings>(configurationRoot.GetSection(nameof(AppSettings)));
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
