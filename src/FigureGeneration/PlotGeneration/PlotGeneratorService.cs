namespace FigureGeneration.Services;

public class PlotGeneratorService
{
	private readonly List<IPlotGenerator> _plotGenerators;

	public PlotGeneratorService(IEnumerable<IPlotGenerator> plotGenerators)
	{
		_plotGenerators = plotGenerators.ToList();
	}

	public async Task GenerateAllPlots()
	{
		Task[] tasks = _plotGenerators.Select(pg => pg.Generate()).ToArray();
		await Task.WhenAll(tasks);
	}
}
