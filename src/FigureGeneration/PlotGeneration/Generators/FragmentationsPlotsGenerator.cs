using FigureGeneration.PlotGeneration;
using JetBrains.Annotations;

namespace FigureGeneration.Services.Generators;

[UsedImplicitly]
public class FragmentationsPlotsGenerator: IPlotGenerator
{
	public void Generate() => Console.WriteLine(nameof(FragmentationsPlotsGenerator));
}
