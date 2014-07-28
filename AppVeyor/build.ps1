$solutionDir = ""
$solutionName = ""

# it is required that project names start with Burgerama
$projectNameParts = $env:APPVEYOR_PROJECT_NAME.split(".")
if ($projectNameParts[0] -ne "Burgerama") {
    Add-AppveyorMessage "Invalid project name" -Category Error
    Break
}

# Burgerama.Web.{Name?}
if ($projectNameParts[1] -eq "Web") {
    $solutionDir = "Web"
    $solutionName = "Burgerama.Web"
}
# Burgerama.Services.{Name}
ElseIf ($projectNameParts[1] -eq "Services") {
    $project = $projectNameParts[2];
    $solutionDir = "Services\$project"
    $solutionName = "Burgerama.Services.$project.sln"
}
Else {
    Add-AppveyorMessage "Invalid project name" -Category Error
    Break
}

nuget restore "$env:APPVEYOR_BUILD_FOLDER\$solutionDir\$solutionName"
msbuild "$env:APPVEYOR_BUILD_FOLDER\$solutionDir\$solutionName" /verbosity:minimal /logger:"C:\Program Files\AppVeyor\BuildAgent\Appveyor.MSBuildLogger.dll" /p:DeployOnBuild=True /p:PublishProfile=appveyor