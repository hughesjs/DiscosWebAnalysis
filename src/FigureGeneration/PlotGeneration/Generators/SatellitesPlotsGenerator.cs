using System.Collections.ObjectModel;
using DiscosWebSdk.Enums;
using DiscosWebSdk.Interfaces.Clients;
using DiscosWebSdk.Models.ResponseModels.DiscosObjects;
using FigureGeneration.Data;
using FigureGeneration.Services;
using JetBrains.Annotations;

namespace FigureGeneration.PlotGeneration.Generators;

[UsedImplicitly]
public class SatellitesPlotsGenerator : IPlotGenerator
{
	private readonly ArchivedDataRepository _dataRepository;
	
	public SatellitesPlotsGenerator(ArchivedDataRepository dataRepository, IDiscosClient discosClient)
	{
		_dataRepository    = dataRepository;
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
