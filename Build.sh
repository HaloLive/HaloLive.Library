dotnet restore
dotnet build HaloLive.Library.sln

xbuild ./tests/HaloLive.Models.Tests/HaloLive.Models.Tests.csproj /p:DebugSymbols=False