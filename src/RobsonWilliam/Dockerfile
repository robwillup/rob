FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /app
COPY RobsonWilliam.csproj .
RUN dotnet restore "RobsonWilliam.csproj"
COPY . .

FROM build AS publish
WORKDIR /app
RUN dotnet publish "RobsonWilliam.csproj" -c Release -o publish

FROM nginx:alpine AS final
WORKDIR /var/www/web
RUN ls -al
COPY --from=publish /app/publish/wwwroot .
RUN ls -al
COPY nginx.conf /etc/nginx/nginx.conf