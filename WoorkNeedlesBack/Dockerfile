# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar todos los .csproj y restaurar dependencias
COPY WoorkNeedles/WoorkNeedles.csproj ./WoorkNeedles/
COPY Application/Application.csproj ./Application/
COPY Domain/Domain.csproj ./Domain/
COPY Infraestructure/Infraestructure.csproj ./Infraestructure/

RUN dotnet restore ./WoorkNeedles/WoorkNeedles.csproj

# Copiar todo el código y publicar
COPY . .
RUN dotnet publish WoorkNeedles/WoorkNeedles.csproj -c Release -o /out

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "WoorkNeedles.dll"]