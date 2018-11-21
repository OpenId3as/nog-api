FROM microsoft/aspnetcore:2.2
ARG source
WORKDIR /app
EXPOSE 80
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.dll"]
