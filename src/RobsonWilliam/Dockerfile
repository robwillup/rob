FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /app
COPY RobsonWilliam.csproj .
RUN dotnet restore "RobsonWilliam.csproj"
COPY . .
RUN dotnet build "RobsonWilliam.csproj" -c Release -o build

FROM build AS publish
RUN dotnet publish "RobsonWilliam.csproj" -c Release -o publish

FROM nginx:alpine AS final
WORKDIR /var/www/web
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf