using JetBrains.Annotations;

namespace FigureGeneration.Services.Generators;

[UsedImplicitly]
public class SatellitesPlotsGenerator: IPlotGenerator
{
	public async Task Generate() => Console.WriteLine(nameof(SatellitesPlotsGenerator));
}