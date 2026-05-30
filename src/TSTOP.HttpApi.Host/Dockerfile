FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore TSTOP.slnx
RUN dotnet publish src/TSTOP.HttpApi.Host/TSTOP.HttpApi.Host.csproj -c Release -o /app/publish /p:UseAppHost=false
RUN dotnet dev-certs https -v -ep /app/publish/openiddict.pfx -p cb879b06-0887-4986-9f72-4a70d2c4c834

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "TSTOP.HttpApi.Host.dll"]