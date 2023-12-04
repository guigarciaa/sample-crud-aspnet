FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5050

# ENV ASPNETCORE_URLS=http://+:5050

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG configuration=Release
WORKDIR /src
# COPY ["src/SampleCrud.API/SampleCrud.API.csproj", "app/src/SampleCrud.API/"]
# RUN dotnet restore "app/src/SampleCrud.API/SampleCrud.API.csproj"
COPY . .
# WORKDIR "/src/app/src/SampleCrud.API"
RUN dotnet restore "SampleCrud.Application/SampleCrud.Application.csproj"
RUN dotnet restore "SampleCrud.Domain/SampleCrud.Domain.csproj"
RUN dotnet restore "SampleCrud.Data/SampleCrud.Data.csproj"
RUN dotnet restore "SampleCrud.Infra/SampleCrud.Infra.csproj"
RUN dotnet restore "SampleCrud.API/SampleCrud.API.csproj"
RUN dotnet build -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SampleCrud.API.dll"]