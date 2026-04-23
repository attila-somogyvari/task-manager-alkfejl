FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["TaskManager.sln", "./"]
COPY ["source/Domain/Domain.csproj", "source/Domain/"]
COPY ["source/TaskService/TaskService.csproj", "source/TaskService/"]

RUN dotnet restore "source/TaskService/TaskService.csproj"

COPY . .
RUN dotnet publish "source/TaskService/TaskService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "TaskService.dll"]
