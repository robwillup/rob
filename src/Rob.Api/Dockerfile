FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine3.17 AS build
WORKDIR /app
COPY Rob.Api.csproj .
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o build

FROM build AS publish
WORKDIR /app
RUN dotnet publish -c Release -o publish -r linux-musl-x64 --self-contained

FROM mcr.microsoft.com/dotnet/runtime:7.0-alpine AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "./Rob.Api" ]
