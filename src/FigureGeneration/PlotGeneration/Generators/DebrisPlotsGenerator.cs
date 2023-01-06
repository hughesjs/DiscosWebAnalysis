using FigureGeneration.PlotGeneration;
using JetBrains.Annotations;

namespace FigureGeneration.Services.Generators;

[UsedImplicitly]
public class DebrisPlotsGenerator: IPlotGenerator
{

	public void Generate() => Console.WriteLine(nameof(DebrisPlotsGenerator));
}
