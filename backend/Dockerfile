FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY Directory.Packages.props ./
COPY backend/*.csproj ./backend/
RUN dotnet restore ./backend/backend.csproj

COPY . ./
RUN dotnet publish ./backend/backend.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "backend.dll"]
