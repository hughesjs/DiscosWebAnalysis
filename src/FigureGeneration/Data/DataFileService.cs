using System.Text.RegularExpressions;
using DiscosWebSdk.Models.ResponseModels;
using FigureGeneration.Options;
using Microsoft.Extensions.Options;

namespace FigureGeneration.Data;

public class DataFileService
{
	private readonly IOptions<AppSettings> _appSettings;
	public DataFileService(IOptions<AppSettings> appSettings)
	{
		_appSettings = appSettings;
	}

	public string GetDataFilePath<T>() where T: DiscosModelBase
	{
		string dataRoot     = _appSettings.Value.DataRoot;
		string dataFolder   = GetLatestFolder(dataRoot);
		string dataFilePath = GetDataFileForType<T>(dataFolder);

		return dataFilePath;
	}

	private string GetDataFileForType<T>(string dataFolder) where T: DiscosModelBase
	{
		string?[] dataFileNames = Directory.GetFiles(dataFolder, "*.json").Select(Path.GetFileName).ToArray();

		if (dataFileNames is null)
		{
			throw new($"Data file for {typeof(T)} in {dataFolder} not found");
		}

		Regex dataFileRegex = new(@$"discos-{typeof(T).Name}-\d{{8}}.json");

		string relevantDataFile = dataFileNames.Single(dataFileRegex.IsMatch);
		string dataFilePath     = Path.Combine(dataFolder, relevantDataFile);

		return dataFilePath;
	}

	private string GetLatestFolder(string dataRoot)
	{
		Regex      dateRegex   = new(@"\d{8}");
		string[]   directories = Directory.GetDirectories(dataRoot);
		DateOnly[] folderDates = directories.Select(dir => new DirectoryInfo(dir))
											.Where(di => dateRegex.IsMatch(di.Name))
											.Select(di => DateOnly.ParseExact(di.Name, "yyyyMMdd"))
											.ToArray();
		DateOnly latestDate = folderDates.Max();
		return Path.Combine(dataRoot, latestDate.ToString("yyyyMMdd"));
	}
}
