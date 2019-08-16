FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
ADD  WebAPI/bin/Release/netcoreapp2.2 /app
ENTRYPOINT ["dotnet", "WebAPI.dll"]