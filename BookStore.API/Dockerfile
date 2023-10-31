#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BookStore.API/BookStore.API.csproj", "BookStore.API/"]
COPY ["BookStoreApplication/BookStoreApplication.csproj", "BookStoreApplication/"]
COPY ["BookStoreDomain/BookStoreDomain.csproj", "BookStoreDomain/"]
COPY ["BookStorePersistence/BookStorePersistence.csproj", "BookStorePersistence/"]
RUN dotnet restore "BookStore.API/BookStore.API.csproj"
COPY . .
WORKDIR "/src/BookStore.API"
RUN dotnet build "BookStore.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookStore.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookStore.API.dll"]