# escape=`
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS builder
EXPOSE 80
EXPOSE 443
WORKDIR /app
COPY . /app

RUN nuget restore USite-Templating.sln

RUN msbuild USite-Templating.sln /p:Configuration=Release `
                                      /p:DeployOnBuild=True `
                                      /p:DeployDefaultTarget=WebPublish `
                                      /p:WebPublishMethod=FileSystem `
                                      /p:DeleteExistingFiles=True `
                                      /p:publishUrl=c:\out

FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8-windowsservercore-ltsc2019
WORKDIR c:/inetpub/wwwroot
COPY --from=builder c:/out .