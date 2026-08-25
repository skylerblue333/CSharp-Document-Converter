FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY CSharp-Document-Converter.csproj ./
RUN dotnet restore CSharp-Document-Converter.csproj
COPY Program.cs TextTransforms.cs ./
RUN dotnet publish CSharp-Document-Converter.csproj -c Release --no-restore -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=8080
WORKDIR /app
COPY --from=build /app/publish ./
USER app
EXPOSE 8080
ENTRYPOINT ["dotnet", "CSharp-Document-Converter.dll"]
