del /q "D:\Projetos\arquitetura-corporativa\src\Calculo\package-infra\netcoreapp3.1"
del /q "D:\Projetos\arquitetura-corporativa\src\RH\package-infra\netcoreapp3.1"
del /q "D:\Projetos\arquitetura-corporativa\src\RHQuery\package-infra\netcoreapp3.1"

del /q "D:\Projetos\arquitetura-corporativa\src\Calculo\package-identity\netcoreapp3.1"
del /q "D:\Projetos\arquitetura-corporativa\src\RH\package-identity\netcoreapp3.1"
del /q "D:\Projetos\arquitetura-corporativa\src\RHQuery\package-identity\netcoreapp3.1"

xcopy /S /D /Y "D:\Projetos\arquitetura-corporativa\src\Infra\package\netcoreapp3.1" "D:\Projetos\arquitetura-corporativa\src\Calculo\package-infra\netcoreapp3.1"
xcopy /S /D /Y "D:\Projetos\arquitetura-corporativa\src\Identity\package\netcoreapp3.1" "D:\Projetos\arquitetura-corporativa\src\Calculo\package-identity\netcoreapp3.1"

xcopy /S /D /Y "D:\Projetos\arquitetura-corporativa\src\Infra\package\netcoreapp3.1" "D:\Projetos\arquitetura-corporativa\src\RH\package-infra\netcoreapp3.1"
xcopy /S /D /Y "D:\Projetos\arquitetura-corporativa\src\Identity\package\netcoreapp3.1" "D:\Projetos\arquitetura-corporativa\src\RH\package-identity\netcoreapp3.1"

xcopy /S /D /Y "D:\Projetos\arquitetura-corporativa\src\Infra\package\netcoreapp3.1" "D:\Projetos\arquitetura-corporativa\src\RHQuery\package-infra\netcoreapp3.1"
xcopy /S /D /Y "D:\Projetos\arquitetura-corporativa\src\Identity\package\netcoreapp3.1" "D:\Projetos\arquitetura-corporativa\src\RHQuery\package-identity\netcoreapp3.1"

pause