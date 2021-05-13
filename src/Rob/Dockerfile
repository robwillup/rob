FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /app
COPY Rob.csproj .
RUN dotnet restore
COPY . .

FROM build AS publish
WORKDIR /app
RUN dotnet publish -c Release -o publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf