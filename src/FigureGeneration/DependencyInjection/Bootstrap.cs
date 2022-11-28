using DiscosGroove.Main.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FigureGeneration;

public static class Bootstrap
{
	public static ServiceProvider Up()
	{
		// IConfigurationBuilder builder = new ConfigurationBuilder();
		//
		// IConfigurationRoot? config = builder.Build();

		IServiceCollection services = new ServiceCollection()
		   .AddServices();

		return services.BuildServiceProvider();
	}
}
