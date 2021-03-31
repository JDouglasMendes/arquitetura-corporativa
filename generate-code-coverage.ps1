#CREDITOS: https://gist.github.com/lira92/29aca1c01661a8de30890a6538c0ba5c

param (
    [string]$format = "opencover",
    [bool]$nohtml = $false
)

if (-Not (Get-Command -Name reportgenerator -ErrorAction SilentlyContinue))
{
    Write-Output "Installing reportgenerator"
    Invoke-Expression "dotnet tool install --global dotnet-reportgenerator-globaltool --version 4.0.9"
}

function Get-BaseCommand {
    Param ([string]$basename, [string]$name)
    return "dotnet test ./test/$($BaseName)/$($Name) /p:Exclude='[xunit.*]*' /p:CollectCoverage=true";
}

Write-Output "Getting csproj files"
$reports = [System.Collections.ArrayList]@()
$count = Get-ChildItem -Path .\test -Recurse -Filter *.csproj -File | Measure-Object | ForEach-Object{$_.Count}
Get-ChildItem -Path .\test -Recurse -Filter *.csproj -File | ForEach-Object {
    Write-Output "Running tests of project $($_.BaseName)/$($_.Name)"
    if ($reports.Count -eq $count-1) {
        Write-Output "Generating unified report using $($format) format"
        Invoke-Expression "$(Get-BaseCommand -basename $_.BaseName -name $_.Name) /p:MergeWith='..\result.json' /p:CoverletOutput='..\coverage.$($format).xml' /p:CoverletOutputFormat='$($format)'"
    }
    elseif ($reports.Count -gt 0) {
        Write-Output "Merging results coverage with project $($reports[$reports.Count-1])"
        Invoke-Expression "$(Get-BaseCommand -basename $_.BaseName -name $_.Name) /p:MergeWith='..\result.json' /p:CoverletOutput='..\result.json'"
    } else {
        Write-Output "================================================================="
        Invoke-Expression "$(Get-BaseCommand -basename $_.BaseName -name $_.Name) /p:CoverletOutput='..\result.json'"
    }

    $reports.Add(".\test\$($_.BaseName)")
}

$reportsText = $reports -join "`n"

Write-Output "Run tests finished. Tested projects: `n$($reportsText)"

if ($nohtml -eq $false) {
    Invoke-Expression "reportgenerator '-reports:./test/coverage.opencover.xml' '-targetdir:./report'"
    Invoke-Item ./report/index.htm
}