using System.Text.Json;
using DiscosWebSdk.Models.ResponseModels.DiscosObjects;

namespace FigureGeneration.Data;

public class ArchivedDataRepository
{
	private readonly DataFileService _dataFileService;

	public readonly Lazy<DiscosObject[]> DiscosObjects;

	public ArchivedDataRepository(DataFileService dataFileService)
	{
		_dataFileService = dataFileService;

		DiscosObjects = new(GetDiscosObjects, LazyThreadSafetyMode.ExecutionAndPublication);
	}


	private DiscosObject[] GetDiscosObjects()
	{
		string filePath = _dataFileService.GetDataFilePath<DiscosObject>();
		using FileStream fStream = new(filePath, FileMode.Open);
		DiscosObject[]? discosObjects = JsonSerializer.Deserialize<DiscosObject[]>(fStream);

		if (discosObjects is null)
		{
			throw new("Deserialisation of DiscosObjects failed");
		}

		return discosObjects.OrderBy(s => s.SatNo).ToArray();
	}
}