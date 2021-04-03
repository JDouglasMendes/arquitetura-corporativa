dotnet test AllTest.sln --collect:"XPlat Code Coverage"
reportgenerator "-reports:./**/coverage.cobertura.xml" "-targetdir:./TestResults/" -reporttypes:Html
Invoke-Item "./TestResults/index.html"
