using System.Text.Json;
using System.Text.Json.Serialization;
using DiscosWebSdk.Models.ResponseModels.DiscosObjects;
using FigureGeneration.Options;
using Microsoft.Extensions.Options;

namespace FigureGeneration.Data;


public class ArchivedDataRepository
{
	private readonly IOptions<AppSettings> _appSettings;
	private readonly DataFileService       _dataFileService;

	public readonly Lazy<Task<DiscosObject[]>> DiscosObjects;

	public ArchivedDataRepository(IOptions<AppSettings> appSettings, DataFileService dataFileService)
	{
		_appSettings     = appSettings;
		_dataFileService = dataFileService;

		DiscosObjects = new(GetDiscosObjects, LazyThreadSafetyMode.ExecutionAndPublication);
	}


	private async Task<DiscosObject[]> GetDiscosObjects()
	{
		string                 filePath      = _dataFileService.GetDataFilePath<DiscosObject>();
		await using FileStream fStream       = new(filePath, FileMode.Open);
		DiscosObject[]?         discosObjects = await JsonSerializer.DeserializeAsync<DiscosObject[]>(fStream);

		if (discosObjects is null)
		{
			throw new("Deserialisation of DiscosObjects failed");
		}

		return discosObjects;
	}

}

