FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["TaskManager.sln", "./"]
COPY ["source/Domain/Domain.csproj", "source/Domain/"]
COPY ["source/McpService/McpService.csproj", "source/McpService/"]

RUN dotnet restore "source/McpService/McpService.csproj"

COPY . .
RUN dotnet publish "source/McpService/McpService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "McpService.dll"]
