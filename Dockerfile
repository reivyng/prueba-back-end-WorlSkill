# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar csproj y restaurar dependencias
COPY prueba.sln .
COPY Web/Web.csproj ./Web/
COPY Business/Business.csproj ./Business/
COPY Data/Data.csproj ./Data/
COPY Utilities/Utilities.csproj ./Utilities/
RUN dotnet restore

# Copiar el resto del código y compilar
COPY . .
WORKDIR /src/Web
RUN dotnet publish -c Release -o /app

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app ./

# Exponer puerto
EXPOSE 8080
ENTRYPOINT ["dotnet", "Web.dll"]
