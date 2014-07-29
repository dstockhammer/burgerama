$endpointName = "$env:APPVEYOR_PROJECT_NAME.Endpoint"
$endpointArtifact = "$env:APPVEYOR_BUILD_FOLDER\$endpointName.zip"

if (Test-Path -path $endpointArtifact) {    
    $moduleDir = "C:\SSH-Sessions"
    $zipName = "SSH-SessionsPSv3.zip"
    $zipUrl = "http://www.powershelladmin.com/w/images/a/a5/SSH-SessionsPSv3.zip"

    Write-Host "Downloading SSH-Sessions..."
    (New-Object System.Net.WebClient).DownloadFile($zipUrl, "$env:APPVEYOR_BUILD_FOLDER\$zipName")

    Write-Host "Extracting SSH-Sessions to $moduleDir..."
    Add-Type -As System.IO.Compression.FileSystem
    $zipFile = Get-Item "$env:APPVEYOR_BUILD_FOLDER\$zipName"
    [IO.Compression.ZipFile]::ExtractToDirectory($zipFile, $moduleDir)

    Remove-Module SSH-Sessions -ErrorAction SilentlyContinue
    Import-Module "$moduleDir\SSH-Sessions"
    New-SshSession -ComputerName $env:SSH_IP -Username $env:SSH_USERNAME -Password $env:SSH_PASSWORD

    Execute-Command("cd ~")
    Execute-Command("forever stop -c mono environments/$endpointName/$endpointName.exe")
    Execute-Command("rm -R environments/$endpointName")
    Execute-Command("unzip deployments/endpointName.zip -d environments/$endpointName")
    Execute-Command("sed -i 's/Config\\/Config\//g' environments/$endpointName/$endpointName.exe.config")
    Execute-Command("forever start -c mono environments/$endpointName/$endpointName.exe")
    Execute-Command("exit")
}

function Execute-Command($command) {
    Write-Host $command
    $output = Invoke-SshCommand -ComputerName $env:SSH_IP -Quiet -Command $command
    
    return $output
}