FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5036

ENV ASPNETCORE_URLS=http://+:5036

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Nyneo Web.csproj", "./"]
RUN dotnet restore "Nyneo Web.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Nyneo Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Nyneo Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nyneo Web.dll"]
