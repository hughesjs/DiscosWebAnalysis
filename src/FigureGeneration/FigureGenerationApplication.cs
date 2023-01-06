using FigureGeneration.PlotGeneration;
using FigureGeneration.Services;
using Microsoft.Extensions.Logging;

namespace FigureGeneration;

public class FigureGenerationApplication
{
	private readonly IEnumerable<IPlotGenerator> _plotGenerators;
	private readonly ILogger<FigureGenerationApplication> _logger;

	public FigureGenerationApplication(IEnumerable<IPlotGenerator> plotGenerators, ILogger<FigureGenerationApplication> logger)
	{
		_plotGenerators = plotGenerators;
		_logger = logger;
	}

	public void Run()
	{
		_logger.LogInformation("Beginning plot generation");
		foreach (IPlotGenerator plotGenerator in _plotGenerators)
		{
			_logger.LogInformation("Starting generation for {PlotGeneratorName}", plotGenerator.GetType().Name);
			plotGenerator.Generate();
		}
	}
}