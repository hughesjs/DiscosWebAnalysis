using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FigureGeneration.DependencyInjection;

public static class Bootstrap
{
	public static ServiceProvider Up()
	{
		IConfigurationBuilder builder = new ConfigurationBuilder()
		   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
		   .AddEnvironmentVariables();
		
		IConfigurationRoot config = builder.Build();

		IServiceCollection services = new ServiceCollection()
		   .AddServices(config);

		return services.BuildServiceProvider();
	}
}
