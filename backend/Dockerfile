FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["todo-app.csproj", "./"]
RUN dotnet restore "./todo-app.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "todo-app.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "todo-app.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "todo-app.dll"]
