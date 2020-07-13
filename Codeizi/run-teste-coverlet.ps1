dotnet test codeizi.sln --logger:trx --results-directory ../TestResults  /p:CollectCoverage=true  /p:CoverletOutput=../TestResults/  /p:MergeWith=../TestResults/coverlet.json  /p:CoverletOutputFormat=json%2ccobertura 


reportgenerator "-reports:./TestResults/coverage.cobertura.xml" "-targetdir:./TestResults/" -reporttypes:Html

#reportgenerator "-reports:./Codeizi.Curso.Domain.SharedKernel.Test/TestResults/coverage.opencover.xml" "-targetdir:./Codeizi.Curso.Domain.SharedKernel.Test/TestResults/" -reporttypes:Html
#reportgenerator "-reports:./Codeizi.Curso.Domain.Test/TestResults/coverage.opencover.xml" "-targetdir:./Codeizi.Curso.Domain.Test/TestResults/" -reporttypes:Html

Invoke-Item "./TestResults/index.html"

pause
