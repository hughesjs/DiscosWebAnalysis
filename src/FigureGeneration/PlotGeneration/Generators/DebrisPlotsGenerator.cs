using JetBrains.Annotations;

namespace FigureGeneration.Services.Generators;

[UsedImplicitly]
public class DebrisPlotsGenerator: IPlotGenerator
{

	public async Task Generate() => Console.WriteLine(nameof(DebrisPlotsGenerator));
}
