FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Main.API/Main.API.csproj", "Main.API/"]
RUN dotnet restore "Main.API/Main.API.csproj"
COPY . .
WORKDIR "/src/Main.API"
RUN dotnet build "Main.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Main.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Main.API.dll"]
