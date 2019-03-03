FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app
# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore
# Copy everything else and build
COPY . ./
# The appsettings.json is the prod settings and at the moment it is set for all logging
# I'm not sure how to publish without setting to prod mode - this is an easy workaround
RUN dotnet publish --output out
# Create the sql scripts that would be made from dotnet ef database update
RUN dotnet ef migrations script --output migration-scripts/creation-script.sql

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime AS app-server
WORKDIR /app
COPY --from=build-env /app/out .
# Copy database initializer script to app-server
COPY --from=build-env /app/migration-scripts ./migration-scripts
ENTRYPOINT ["dotnet", "memento-scraper-api.dll"]