using FigureGeneration.Services;

namespace FigureGeneration;

public class FigureGenerationApplication
{
	private readonly IEnumerable<IPlotGenerator> _plotGenerators;

	public FigureGenerationApplication(IEnumerable<IPlotGenerator> plotGenerators)
	{
		_plotGenerators = plotGenerators;
	}

	public void Run()
	{
		foreach (IPlotGenerator plotGenerator in _plotGenerators)
		{
			plotGenerator.Generate();
		}
	}
}