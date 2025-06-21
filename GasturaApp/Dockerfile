FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia a solution e o projeto (ambos estão na mesma pasta)
COPY ["GasturaApp.sln", "./"]
COPY ["GasturaApp.csproj", "./"]

RUN dotnet restore "GasturaApp.sln"

COPY . .

RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "GasturaApp.dll"]
