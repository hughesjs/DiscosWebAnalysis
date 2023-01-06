using FigureGeneration;
using FigureGeneration.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

ServiceProvider serviceProvider = Bootstrap.Up();
FigureGenerationApplication app = serviceProvider.GetRequiredService<FigureGenerationApplication>();

app.Run();