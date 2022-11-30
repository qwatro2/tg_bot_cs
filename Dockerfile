FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY TgBot/TgBot.csproj TgBot/
RUN dotnet restore TgBot/TgBot.csproj
COPY . .
WORKDIR /src/TgBot
RUN dotnet build TgBot.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish TgBot.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
EXPOSE 80/tcp
CMD ["dotnet", "exec", "TgBot.dll"]