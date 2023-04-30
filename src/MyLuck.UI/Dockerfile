#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MyLuck.UI/MyLuck.UI.csproj", "MyLuck.UI/"]
RUN dotnet restore "MyLuck.UI/MyLuck.UI.csproj"
COPY . .
WORKDIR "/src/MyLuck.UI"
RUN dotnet build "MyLuck.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyLuck.UI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyLuck.UI.dll"]