using System.Text.Json;
using DiscosWebSdk.Models.ResponseModels.DiscosObjects;

namespace FigureGeneration.Data;


public class ArchivedDataRepository
{
	private readonly DataFileService       _dataFileService;

	public readonly Lazy<Task<DiscosObject[]>> DiscosObjects;

	public ArchivedDataRepository(DataFileService dataFileService)
	{
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

		return discosObjects.OrderBy(s => s.SatNo).ToArray();
	}

}

