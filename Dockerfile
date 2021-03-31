FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
RUN apt-get update && apt-get dist-upgrade -y && apt-get install -y openjdk-11-jre
COPY . ./
RUN dotnet tool install -g dotnet-sonarscanner
ENV PATH="${PATH}:/root/.dotnet/tools"

#RUN dotnet test ./src/Calculo/test/CalculoFolhaDePagamento.Domain.Test/CalculoFolhaDePagamento.Test.csproj /p:Exclude='[xunit.*]*' /p:CollectCoverage=true /p:CoverletOutput='./test/result.json'
#RUN dotnet test ./test/Codeizi.Curso.Domain.Test/Codeizi.Curso.Domain.Test.csproj /p:MergeWith='..\result.json' /p:CoverletOutput='..\result.json'
#RUN dotnet test ./test/Codeizi.Curso.Domain.SharedKernel.Test/Codeizi.Curso.Domain.SharedKernel.Test.csproj /p:MergeWith='..\result.json' /p:CoverletOutput='..\result.json'
#RUN dotnet test ./test/Codeizi.Curso.CalculoFolhaDePagamento.Test/Codeizi.Curso.CalculoFolhaDePagamento.Test.csproj /p:MergeWith='..\result.json' /p:CoverletOutput='..\result.json'
#RUN dotnet test ./src/RH/test/RH.Domain.Test/RH.Domain.Test.csproj /p:Exclude='[xunit.*]*' /p:CollectCoverage=true /p:MergeWith='./test/result.json' /p:CoverletOutput='./test/coverage.opencover.xml' /p:CoverletOutputFormat='opencover'

RUN dotnet test AllTest.sln /p:CollectCoverage=true /p:CoverletOutput='..\..\..\..\coverage.opencover.xml' /p:MergeWith='..\..\..\..\coverage.json' /p:CoverletOutputFormat='opencover'

RUN dotnet sonarscanner begin /k:"ambev-tech" \
    /d:sonar.host.url="http://192.168.0.10:9000" \    
    /d:sonar.verbose=true \
    /v:1.0.0 \
    /d:sonar.login="e02e2079f18bab591442eebfa00a9ae90bc32ba2" \
    /d:sonar.cs.opencover.reportsPaths="./coverage.opencover.xml" 
    #/d:sonar.coverage.exclusions="**Test.cs"
RUN dotnet build "All.sln"
RUN dotnet sonarscanner end /d:sonar.login="e02e2079f18bab591442eebfa00a9ae90bc32ba2"