#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["auth/IdentityServer/IdentityServer.csproj", "auth/IdentityServer/"]
COPY ["auth/Infrastructure/Infrastructure.csproj", "auth/Infrastructure/"]
COPY ["common/Application/Application.Common.csproj", "common/Application/"]
COPY ["auth/Domain/Domain.csproj", "auth/Domain/"]
COPY ["auth/Application/Application.csproj", "auth/Application/"]
COPY ["common/Infrastructure/Infrastructure.Common.csproj", "common/Infrastructure/"]
COPY ["common/Domain.Common/Domain.Common.csproj", "common/Domain.Common/"]
RUN dotnet restore "auth/IdentityServer/IdentityServer.csproj"
COPY . .
WORKDIR "/src/auth/IdentityServer"
RUN dotnet build "IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityServer.dll"]