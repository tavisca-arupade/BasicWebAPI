FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY  BasicWebAPI/Publish/* /app
ENTRYPOINT ["dotnet", "WebAPI.dll"]