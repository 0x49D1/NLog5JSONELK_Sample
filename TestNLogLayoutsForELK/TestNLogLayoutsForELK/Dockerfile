FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TestNLogLayoutsForELK.csproj", "TestNLogLayoutsForELK/"]
RUN dotnet restore "TestNLogLayoutsForELK/TestNLogLayoutsForELK.csproj"
COPY . .
WORKDIR "/src/TestNLogLayoutsForELK"
RUN dotnet build "TestNLogLayoutsForELK.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestNLogLayoutsForELK.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestNLogLayoutsForELK.dll"]
