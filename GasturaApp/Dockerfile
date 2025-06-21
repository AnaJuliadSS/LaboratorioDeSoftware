# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copiar apenas os arquivos da solution e dos projetos para restaurar as dependências
COPY GasturaApp.sln ./
COPY GasturaApp/ ./GasturaApp/
COPY GasturaAppTest/ ./GasturaAppTest/

# Restaurar todas as dependências da solution
RUN dotnet restore GasturaApp.sln

# Build só do projeto GasturaApp, com Release
RUN dotnet build ./GasturaApp/GasturaApp.csproj -c Release -o /app/build

# Etapa 2: Publish (publicação do projeto que será executado)
RUN dotnet publish ./GasturaApp/GasturaApp.csproj -c Release -o /app/publish /p:UseAppHost=false

# Etapa 3: Runtime - imagem final enxuta para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# Copiar arquivos publicados da etapa de build
COPY --from=build /app/publish .

# Expor a porta padrão (ajuste se seu app usa outra)
EXPOSE 80

# Definir entrypoint para rodar a aplicação
ENTRYPOINT ["dotnet", "GasturaApp.dll"]
