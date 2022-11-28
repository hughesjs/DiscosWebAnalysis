FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FigureGeneration/FigureGeneration.csproj", "FigureGeneration/"]
RUN dotnet restore "FigureGeneration/FigureGeneration.csproj"
COPY . .
WORKDIR "/src/FigureGeneration"
RUN dotnet build "FigureGeneration.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FigureGeneration.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FigureGeneration.dll"]
