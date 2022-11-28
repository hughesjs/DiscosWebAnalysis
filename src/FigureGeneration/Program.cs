// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using DiscosWebSdk.Models.ResponseModels.DiscosObjects;
using FigureGeneration;
using FigureGeneration.Services;
using Microsoft.Extensions.DependencyInjection;

ServiceProvider serviceProvider = Bootstrap.Up();
PlotGeneratorService pgService = serviceProvider.GetRequiredService<PlotGeneratorService>();
await pgService.GenerateAllPlots();


const string dataRoot      = "/home/james/repos/DiscosWebAnalysis/data/";
string discosObjectsJson = File.ReadAllText($"{dataRoot}20221125/discos-DiscosObject-20221125.json");

List<DiscosObject>? discosObjects = JsonSerializer.Deserialize<List<DiscosObject>>(discosObjectsJson);

