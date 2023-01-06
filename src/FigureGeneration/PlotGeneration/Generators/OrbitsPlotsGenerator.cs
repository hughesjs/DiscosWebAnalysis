using FigureGeneration.PlotGeneration;
using JetBrains.Annotations;

namespace FigureGeneration.Services.Generators;

[UsedImplicitly]
public class OrbitsPlotsGenerator: IPlotGenerator
{
	public void Generate() => Console.WriteLine(nameof(OrbitsPlotsGenerator));
}
