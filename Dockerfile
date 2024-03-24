FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy everything
COPY . ./
# Restore as distinct layers
RUN find . -name '*.csproj' -type f -print0 | xargs -0 -n1 dotnet restore
# Build and publish a release
RUN find . -name '*.csproj' -type f -print0 | xargs -0 -n1 dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Ces.Api.dll"]
