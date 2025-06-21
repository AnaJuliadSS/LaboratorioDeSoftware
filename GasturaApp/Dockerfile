FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas os arquivos do projeto principal e da solution
COPY ["GasturaApp/GasturaApp.csproj", "GasturaApp/"]
COPY ["GasturaApp.sln", "./"]

RUN dotnet restore "GasturaApp.sln"

COPY . .

WORKDIR "/src/GasturaApp"
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "GasturaApp.dll"]
