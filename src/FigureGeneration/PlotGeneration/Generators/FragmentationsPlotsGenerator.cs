using JetBrains.Annotations;

namespace FigureGeneration.Services.Generators;

[UsedImplicitly]
public class FragmentationsPlotsGenerator: IPlotGenerator
{
	public async Task Generate() => Console.WriteLine(nameof(FragmentationsPlotsGenerator));
}
