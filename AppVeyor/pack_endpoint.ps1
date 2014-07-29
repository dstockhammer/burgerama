# it is required that project names start with Burgerama
$projectNameParts = $env:APPVEYOR_PROJECT_NAME.split(".")
if ($projectNameParts[0] -ne "Burgerama") {
    Add-AppveyorMessage "Invalid project name" -Category Error
    Break
}

if ($projectNameParts[1] -ne "Services") {
    Write-Host "Skipping endpoint for non-service project"
}
Else {
    $project = $projectNameParts[2];
    $projectDir = "$env:APPVEYOR_BUILD_FOLDER\Services\$project\Endpoint"
    $projectName = "Burgerama.Services.$project.Endpoint"

    Write-Host "Transforming and packaging $projectName..."
    msbuild "$env:APPVEYOR_BUILD_FOLDER\Configuration\Transform.proj" /target:"Transform" /property:"Configuration=$env:CONFIGURATION;Path=$projectDir;Name=$projectName"
    7z a -tzip "$env:APPVEYOR_BUILD_FOLDER\$projectName.zip" "$projectDir\bin\$env:CONFIGURATION\*"
    Push-AppveyorArtifact "$env:APPVEYOR_BUILD_FOLDER\$projectName.zip" -DeploymentName "Endpoint"
}