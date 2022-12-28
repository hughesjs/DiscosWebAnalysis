using System.Collections.ObjectModel;
using DiscosWebSdk.Enums;
using DiscosWebSdk.Interfaces.Clients;
using DiscosWebSdk.Models.ResponseModels.DiscosObjects;
using FigureGeneration.Data;
using FigureGeneration.Services;
using JetBrains.Annotations;
using PgfPlots.Net.Public.ElementDefinitions;
using PgfPlots.Net.Public.ElementDefinitions.Enums;
using PgfPlots.Net.Public.ElementDefinitions.Options;
using PgfPlots.Net.Public.ElementDefinitions.Plots;
using PgfPlots.Net.Public.ElementDefinitions.Plots.Data;
using PgfPlots.Net.Public.ElementDefinitions.Wrappers;
using PgfPlots.Net.Public.Interfaces.Services;

namespace FigureGeneration.PlotGeneration.Generators;

[UsedImplicitly]
public class SatellitesPlotsGenerator : IPlotGenerator
{
	private readonly ArchivedDataRepository _dataRepository;
	private readonly IPgfPlotSourceGenerator _sourceGenerator;
	
	public SatellitesPlotsGenerator(ArchivedDataRepository dataRepository, IPgfPlotSourceGenerator sourceGenerator)
	{
		_dataRepository    = dataRepository;
		_sourceGenerator = sourceGenerator;
	}

	public async Task Generate()
	{
		DiscosObject[]                   discosObjects = await _dataRepository.DiscosObjects.Value;
		DiscosObject[]                   satellites    = GetSatellitesFromDiscosObjects(discosObjects);
		ReadOnlyCollection<DiscosObject> roSatellites  = new(satellites);

		Task[] generationTasks = {
									 GenerateSatellitesInOrbitOverTime(roSatellites),
								 };
		
		await Task.WhenAll(generationTasks);
	}

	private async Task GenerateSatellitesInOrbitOverTime(ReadOnlyCollection<DiscosObject> roSatellites)
	{
		Dictionary<int, List<DiscosObject>> satellitesByYear = GetSatellitesInOrbitEachYear(roSatellites);
		Dictionary<int, int> numSatellitesByYear = satellitesByYear.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Count);

		Cartesian2<int>[] coordinates = numSatellitesByYear.Select(kvp => new Cartesian2<int>(kvp.Key, kvp.Value))
			.OrderBy(c => c.X).ToArray();

		int earliestYear = numSatellitesByYear.Keys.Min();
		int latestYear = numSatellitesByYear.Keys.Max();
		
		int mostSatellites = numSatellitesByYear.Values.Max();

		AxisOptions axisOptions = new()
		{
			XLabel = "Year",
			YLabel = "Satellites In Orbit",
			XMin = earliestYear,
			XMax = latestYear,
			YMin = 0,
			YMax = mostSatellites
		};

		PlotOptions plotOptions = new()
		{
			Colour = LatexColour.Red,
			Mark = PlotMark.Cross,
			MarkSize = 0.2f
		};

		PlotDefinition plot = new(plotOptions, coordinates);
		
		PgfPlotDefinition plotDefinition = new(axisOptions, new(){plot});

		FigureDefinition figureDefinition = new()
		{
			Caption = "The Number of Satellites In Orbit Over Time",
			Label = "fig:numsatsovertime",
			Plots = new() { plotDefinition }
		};

		string source = _sourceGenerator.GenerateSourceCode(figureDefinition);

		;
	}

	private Dictionary<int, List<DiscosObject>> GetSatellitesInOrbitEachYear(ReadOnlyCollection<DiscosObject> roSatellites)
	{
		
		
		
		Dictionary<int, List<DiscosObject>> satellitesInOrbit = new();

		foreach (DiscosObject discosObject in roSatellites)
		{
			if (discosObject.Launch?.Epoch is null && discosObject.CosparId is null)
			{
				continue;
			}
			
			int launchYear  = discosObject.Launch?.Epoch?.Year ?? GetYearFromCosparId(discosObject.CosparId);
			int reentryYear = discosObject.Reentry?.Epoch.Year ?? DateTime.UtcNow.Year;
			
			for (int year = launchYear; year <= reentryYear; year++)
			{
				if (satellitesInOrbit.ContainsKey(year))
				{
					satellitesInOrbit[year].Add(discosObject);
				}
				else
				{
					satellitesInOrbit.Add(year, new() {discosObject});
				}
			}
		}

		return satellitesInOrbit;
	}

	private DiscosObject[] GetSatellitesFromDiscosObjects(IEnumerable<DiscosObject> discosObjects)
	{
		ParallelQuery<DiscosObject> satellites = discosObjects.AsParallel().Where(IsSatellite);
		return satellites.ToArray();
	}

	private bool IsSatellite(DiscosObject discosObject)
	{
		return discosObject.ObjectClass == ObjectClass.Payload;
	}

	private int GetYearFromCosparId(string cosparId)
	{
		string yearString = cosparId[..4];
		int    year       = int.Parse(yearString);
		return year;
	}
}
