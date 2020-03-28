FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["BlueBoard.API/BlueBoard.API.csproj", "BlueBoard.API/"]
COPY ["BlueBoard.Common/BlueBoard.Common.csproj", "BlueBoard.Common/"]
COPY ["BlueBoard.Contract.Common/BlueBoard.Contract.Common.csproj", "BlueBoard.Contract.Common/"]
COPY ["BlueBoard.Contract.Identity/BlueBoard.Contract.Identity.csproj", "BlueBoard.Contract.Identity/"]
COPY ["BlueBoard.Mail/BlueBoard.Mail.csproj", "BlueBoard.Mail/"]
COPY ["BlueBoard.Module.Common/BlueBoard.Module.Common.csproj", "BlueBoard.Module.Common/"]
COPY ["BlueBoard.Module.Identity/BlueBoard.Module.Identity.csproj", "BlueBoard.Module.Identity/"]
COPY ["BlueBoard.Persistence/BlueBoard.Persistence.csproj", "BlueBoard.Persistence/"]
COPY ["BlueBoard.Persistence.Abstractions/BlueBoard.Persistence.Abstractions.csproj", "BlueBoard.Persistence.Abstractions/"]
COPY ["BlueBoard.Persistence.Postgres/BlueBoard.Persistence.Postgres.csproj", "BlueBoard.Persistence.Postgres/"]
RUN dotnet restore "BlueBoard.API/BlueBoard.API.csproj"
COPY . .
WORKDIR /src/BlueBoard.API
# RUN dotnet build "BlueBoard.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BlueBoard.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BlueBoard.API.dll"]