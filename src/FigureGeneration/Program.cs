using FigureGeneration;
using FigureGeneration.DependencyInjection;
using FigureGeneration.Services;
using Microsoft.Extensions.DependencyInjection;

ServiceProvider serviceProvider = Bootstrap.Up();
PlotGeneratorService pgService = serviceProvider.GetRequiredService<PlotGeneratorService>();

await pgService.GenerateAllPlots();
