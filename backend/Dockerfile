# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /App

# Copia os arquivos do projeto
COPY . ./

# Restaura as dependências
RUN dotnet restore

# Compila e publica em Release
RUN dotnet publish -c Release -o /App/out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /App

# Copia os arquivos da etapa de build
COPY --from=build /App/out .

# Define o ponto de entrada
ENTRYPOINT ["dotnet", "backend.dll"]
