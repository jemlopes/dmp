FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 9005

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY *.sln ./
COPY DesafioMundiPagg/DesafioMundiPagg.csproj DesafioMundiPagg/
RUN dotnet restore
COPY . .
WORKDIR /src/DesafioMundiPagg
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DesafioMundiPagg.dll"]
