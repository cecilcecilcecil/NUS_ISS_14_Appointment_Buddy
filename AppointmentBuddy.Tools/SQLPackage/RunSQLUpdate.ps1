## A. SAMPLE USAGE -for Local DB:
# .\RunSQLUpdate.ps1 -SolutionDirectory "D:\GEMS2Code\GEMS2\" -Configuration "Debug" -DBProjectName "GEMS2.DB" -DBServerName "localhost" -DBServerPort "1433" -DBCatalogName "GEMS2"       -DBUser "sa" -DBPass "P@ssw0rd" -Register "ranelaa"
# .\RunSQLUpdate.ps1 -SolutionDirectory "D:\GEMS2Code\GEMS2\" -Configuration "Debug" -DBProjectName "GEMS2.DB" -DBServerName "localhost" -DBServerPort "1433" -DBCatalogName "GEMS2Public" -DBUser "sa" -DBPass "P@ssw0rd" -Register "ranelaa" -Dacpac "D:\GEMS2Code\GEMS2\GEMS2.DB\bin\Debug\GEMS2Public.DB.dacpac"
# .\RunSQLUpdate.ps1 -SolutionDirectory "D:\GEMS2Code\GEMS2\" -Configuration "Debug" -DBProjectName "GEMS2.DB" -DBServerName "localhost" -DBServerPort "1433" -DBCatalogName "GEMS2" -DBUser "sa" -DBPass "P@ssw0rd" -Register "ranelaa" -Dacpac "D:\GEMS2Code\GEMS2\GEMS2.DB\Snapshots\GEMS2.DB_20190915_21-52-50.dacpac"
# .\RunSQLUpdate.ps1 -SolutionDirectory "D:\GEMS2Code\GEMS2\" -Configuration "Debug" -DBProjectName "GEMS2.DB" -DBServerName "localhost" -DBServerPort "1433" -DBCatalogName "GEMS2" -DBUser "sa" -DBPass "P@ssw0rd" -Register "ranelaa" -NugetDirectory "C:\Users\ranelaa\.nuget\"
# .\RunSQLUpdate.ps1 -SolutionDirectory "D:\GEMS2Code\GEMS2\" -Configuration "Debug" -DBProjectName "GEMS2.DB" -DBServerName "localhost" -DBServerPort "1433" -DBCatalogName "GEMS2" -DBUser "sa" -DBPass "P@ssw0rd" -Register "ranelaa" -SQLPackage "C:\Users\ranelaa\.nuget\packages\sqlpackage.commandline\14.0.3953.4\tools\SqlPackage.exe"

## B. SAMPLE USAGE -for AWS DB:
# .\RunSQLUpdate.ps1 -SolutionDirectory "D:\GEMS2Code\GEMS2\" -Configuration "Debug" -DBProjectName "GEMS2.DB" -DBServerName "gems2.cgdzr6ynegqm.ap-southeast-1.rds.amazonaws.com" -DBServerPort "1433" -DBCatalogName "GEMS2" -DBUser "admin" -DBPass "PaGems_02" -Register "ranelaa" -Dacpac "D:\GEMS2Code\GEMS2\GEMS2.DB\Snapshots\GEMS2.DB_20191002_12-20-59.dacpac"
# .\RunSQLUpdate.ps1 -SolutionDirectory "D:\GEMS2Code\GEMS2\" -Configuration "Debug" -DBProjectName "GEMS2.DB" -DBServerName "gems2-internet.cgdzr6ynegqm.ap-southeast-1.rds.amazonaws.com" -DBServerPort "1433" -DBCatalogName "GEMS2Public" -DBUser "admin" -DBPass "PaGems_02" -Register "ranelaa" -Dacpac "D:\GEMS2Code\GEMS2\GEMS2.DB\Snapshots\GEMS2Public.DB_20191003_16-43-03.dacpac"

param(
    [Parameter(Mandatory=$true)]
    [string]$SolutionDirectory = "D:\GEMS2Code\GEMS2\",
    [Parameter(Mandatory=$true)]
    [string]$Configuration = "Release",
    [Parameter(Mandatory=$true)]
    [string]$DBProjectName="GEMS2.DB",
    [Parameter(Mandatory=$true)]
    [string]$DBServerName="localhost",
    [Parameter(Mandatory=$true)]
    [string]$DBServerPort="1433",
    [Parameter(Mandatory=$true)]
    [string]$DBCatalogName="GEMS2",
    [Parameter(Mandatory=$true)]
    [string]$DBUser="sa",
    [Parameter(Mandatory=$true)]
    [string]$DBPass="P@ssw0rd",
    [Parameter(Mandatory=$true)]
    [string]$Register="Path32",
    [string]$SQLPackage,
    [string]$NugetDirectory,
    [string]$Dacpac
)

function FindTool([string] $FileName)
{
    Get-ChildItem -Path $ToolsDirectory -Include $FileName -File -Recurse -ErrorAction SilentlyContinue | Select-Object -First 1
}

$SourceDacpacFile = [System.IO.Path]::Combine($SolutionDirectory, $DBProjectName, "bin", $Configuration, "$DBProjectName.dacpac")
if(-not [string]::IsNullOrEmpty($Dacpac))
{
	#$Dacpac = $DBProjectName
	$SourceDacpacFile = $Dacpac
#	$SourceDacpacFile = [System.IO.Path]::Combine($SolutionDirectory, $DBProjectName, "bin", $Configuration, "$DBProjectName.dacpac")
} 
#else 
#{
#	$SourceDacpacFile = $Dacpac
#}
if([string]::IsNullOrEmpty($NugetDirectory))
{
	$NugetDirectory = "C:\Users\$Register\.nuget\"
}
$ToolsDirectory = [System.IO.Path]::Combine($NugetDirectory, "packages")
$TargetConnectionString = "Server=$DBServerName,$DBServerPort;Database=$DBCatalogName;User Id=$DBUser;Password="

Write-Output "`r`nTools used:"

if([string]::IsNullOrEmpty($SQLPackage))
{
    $SQLPackage = FindTool("SQLPackage.exe")
}
Write-Output "    SQLPackage: $SQLPackage"


Write-Output "`r`nRunning SQLPackage"

Write-Output "$SQLPackage /Action:Publish /SourceFile:"$SourceDacpacFile" /TargetConnectionString:"$TargetConnectionString"*********"
&$SQLPackage /Action:Publish /SourceFile:"$SourceDacpacFile" /TargetConnectionString:"$TargetConnectionString$DBPass"


