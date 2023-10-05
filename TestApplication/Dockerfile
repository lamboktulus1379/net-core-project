FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TestApplicatin/TestApplicatin.csproj", "TestApplicatin/"]
RUN dotnet restore "TestApplicatin/TestApplicatin.csproj"
COPY . .
WORKDIR "/src/TestApplicatin"
RUN dotnet build "TestApplicatin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestApplicatin.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestApplicatin.dll"]
