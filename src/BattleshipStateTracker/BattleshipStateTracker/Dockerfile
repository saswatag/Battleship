FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BattleshipStateTracker/BattleshipStateTracker.csproj", "BattleshipStateTracker/"]
COPY ["BattleshipStateTrackerTests/BattleshipStateTrackerTests.csproj", "BattleshipStateTrackerTests/"]

#RUN dotnet restore
RUN dotnet restore "BattleshipStateTracker/BattleshipStateTracker.csproj"
RUN dotnet restore "BattleshipStateTrackerTests/BattleshipStateTrackerTests.csproj"

COPY ["BattleshipStateTracker/.", "BattleshipStateTracker/"]
COPY ["BattleshipStateTrackerTests/.", "BattleshipStateTrackerTests/"]

# run the unit tests
WORKDIR /src/BattleshipStateTrackerTests
RUN dotnet test "BattleshipStateTrackerTests.csproj" --logger:trx

# build the Battleship project
WORKDIR /src/BattleshipStateTracker
RUN dotnet build "BattleshipStateTracker.csproj" -c Release -o /app/build

# Build target to run the tests seaprately without building the full image
FROM build AS testrunner
WORKDIR /src/BattleshipStateTrackerTests
RUN dotnet test "BattleshipStateTrackerTests.csproj" --logger:trx

FROM build AS publish
WORKDIR /src/BattleshipStateTracker
RUN dotnet publish "BattleshipStateTracker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "BattleshipStateTracker.dll"]