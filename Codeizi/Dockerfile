FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
RUN apt-get update && apt-get dist-upgrade -y && apt-get install -y openjdk-11-jre
COPY . ./
RUN dotnet tool install -g dotnet-sonarscanner
ENV PATH="${PATH}:/root/.dotnet/tools"

RUN dotnet test ./test/Codeizi.Curso.Api.Integration.Test/Codeizi.Curso.Api.Integration.Test.csproj /p:Exclude='[xunit.*]*' /p:CollectCoverage=true /p:CoverletOutput='..\result.json'
RUN dotnet test ./test/Codeizi.Curso.Domain.Test/Codeizi.Curso.Domain.Test.csproj /p:MergeWith='..\result.json' /p:CoverletOutput='..\result.json'
RUN dotnet test ./test/Codeizi.Curso.Domain.SharedKernel.Test/Codeizi.Curso.Domain.SharedKernel.Test.csproj /p:MergeWith='..\result.json' /p:CoverletOutput='..\result.json'
RUN dotnet test ./test/Codeizi.Curso.CalculoFolhaDePagamento.Test/Codeizi.Curso.CalculoFolhaDePagamento.Test.csproj /p:MergeWith='..\result.json' /p:CoverletOutput='..\result.json'
#RUN dotnet test ./test/Codeizi.Curso.infra.Data.EventSource.Test/Codeizi.Curso.infra.Data.EventSource.Test.csproj /p:MergeWith='..\result.json' /p:CoverletOutput='..\result.json'
RUN dotnet test ./test/Codeizi.Infra.Data.Test/Codeizi.Infra.Data.Test.csproj /p:Exclude='[xunit.*]*' /p:CollectCoverage=true /p:MergeWith='..\result.json' /p:CoverletOutput='..\coverage.opencover.xml' /p:CoverletOutputFormat='opencover'

RUN dotnet sonarscanner begin /k:"codeizi-curso" \
    /d:sonar.host.url="http://192.168.0.10:9000" \    
    /d:sonar.verbose=true \
    /v:1.0.0 \
    /d:sonar.login="db7dd63b7ea250b13d2d7e675a65b38713900228" \
    /d:sonar.cs.opencover.reportsPaths="./test/coverage.opencover.xml" 
    #/d:sonar.coverage.exclusions="**Test.cs"
RUN dotnet build "Codeizi.sln"
RUN dotnet sonarscanner end /d:sonar.login="db7dd63b7ea250b13d2d7e675a65b38713900228"