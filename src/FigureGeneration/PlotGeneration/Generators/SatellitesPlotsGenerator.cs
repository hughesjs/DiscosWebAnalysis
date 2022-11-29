using DiscosWebSdk.Models.ResponseModels.DiscosObjects;
using FigureGeneration.Data;
using JetBrains.Annotations;

namespace FigureGeneration.Services.Generators;

[UsedImplicitly]
public class SatellitesPlotsGenerator: IPlotGenerator
{
	private readonly ArchivedDataRepository _dataRepository;
	public SatellitesPlotsGenerator(ArchivedDataRepository dataRepository) => _dataRepository = dataRepository;

	public async Task Generate()
	{
		DiscosObject[] discosObjects = await _dataRepository.DiscosObjects.Value;
		;
	}
}
