FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy all foldes, files inside SampleCrud/ and Paste in . inside of docker image.
COPY . .

# Restore all packages.
RUN dotnet restore "SampleCrud.Application/SampleCrud.Application.csproj"
RUN dotnet restore "SampleCrud.Domain/SampleCrud.Domain.csproj"
RUN dotnet restore "SampleCrud.Data/SampleCrud.Data.csproj"
RUN dotnet restore "SampleCrud.Infra/SampleCrud.Infra.csproj"
RUN dotnet restore "SampleCrud.API/SampleCrud.API.csproj"

# Build Project
RUN dotnet build

# Build and Publish builded release files.
RUN dotnet publish -c Release -o /out --no-restore

# Build runtime image.
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "SampleCrud.API.dll"]