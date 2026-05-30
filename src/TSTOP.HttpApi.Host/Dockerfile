FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
ARG OPENIDDICT_CERT_PASSWORD
COPY . .
RUN dotnet restore TSTOP.slnx
RUN dotnet publish src/TSTOP.HttpApi.Host/TSTOP.HttpApi.Host.csproj -c Release -o /app/publish /p:UseAppHost=false
RUN test -n "$OPENIDDICT_CERT_PASSWORD"
RUN dotnet dev-certs https -v -ep /app/publish/openiddict.pfx -p "$OPENIDDICT_CERT_PASSWORD"

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "TSTOP.HttpApi.Host.dll"]