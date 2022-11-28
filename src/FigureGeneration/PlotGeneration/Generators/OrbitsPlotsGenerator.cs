using JetBrains.Annotations;

namespace FigureGeneration.Services.Generators;

[UsedImplicitly]
public class OrbitsPlotsGenerator: IPlotGenerator
{
	public async Task Generate() => Console.WriteLine(nameof(OrbitsPlotsGenerator));
}
