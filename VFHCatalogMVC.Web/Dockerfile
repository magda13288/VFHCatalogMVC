# Step 1: Build app
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src

# Copy project
COPY . ./

# Restore injections
RUN dotnet restore

# Publish app in Release mode
RUN dotnet publish -c Release -o /app/publish

# Step 2: Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS final
WORKDIR /app

# Copy published app
COPY --from=build /app/publish .

# Set default port
ENV ASPNETCORE_URLS=http://0.0.0.0:80

# Run app
ENTRYPOINT ["dotnet", "VFHCatalogMVC.Web.dll"]