$solutionDir = ""
$solutionName = ""
$webProjDir = ""
$webProjName = ""
$artifactName = ""

# it is required that project names start with Burgerama
$projectNameParts = $env:APPVEYOR_PROJECT_NAME.split(".")
if ($projectNameParts[0] -ne "Burgerama") {
    Add-AppveyorMessage "Invalid project name" -Category Error
    Break
}

# Burgerama.Web.{Name}
if ($projectNameParts[1] -eq "Web") {
    $name = $projectNameParts[2]
    $solutionDir = "$env:APPVEYOR_BUILD_FOLDER\Web"
    $solutionName = "Burgerama.Web.sln"
	$webProjDir = "$env:APPVEYOR_BUILD_FOLDER\Web\$name"
	$webProjName = "$name.csproj"
	$artifactName = "$env:APPVEYOR_PROJECT_NAME.zip"
}
# Burgerama.Services.{Name}
ElseIf ($projectNameParts[1] -eq "Services") {
    $name = $projectNameParts[2]
    $solutionDir = "$env:APPVEYOR_BUILD_FOLDER\Services\$name"
    $solutionName = "Burgerama.Services.$name.sln"
	$webProjDir = "$solutionDir\Api"
	$webProjName = "Api.csproj"
	$artifactName = "$env:APPVEYOR_PROJECT_NAME.Api.zip"
}
Else {
    Add-AppveyorMessage "Invalid project name" -Category Error
    Break
}

nuget restore "$solutionDir\$solutionName"

Write-Host "Building $solutionName..."
msbuild "$solutionDir\$solutionName" /verbosity:minimal /logger:"C:\Program Files\AppVeyor\BuildAgent\Appveyor.MSBuildLogger.dll" /p:Configuration="$env:CONFIGURATION"

Write-Host "Packaging $webProjName..."
msbuild "$webProjDir\$webProjName" /verbosity:minimal /t:Package /p:PackageLocation="$env:APPVEYOR_BUILD_FOLDER\$artifactName" /p:PackageAsSingleFile=True /p:Configuration="$env:CONFIGURATION"
Push-AppveyorArtifact "$env:APPVEYOR_BUILD_FOLDER\$artifactName" -DeploymentName "Api" -Type WebDeployPackage