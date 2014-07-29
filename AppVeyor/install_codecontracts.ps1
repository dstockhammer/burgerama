$codeContractsDir = "C:\CodeContracts"
$msBuildDir = "C:\Program Files (x86)\MSBuild"
$zipName = "CodeContracts.custom.zip"
$zipUrl = "https://www.dropbox.com/s/pi42642wv2251df/CodeContracts.custom.zip?dl=1"

Write-Host "Downloading CodeContracts..."
(New-Object System.Net.WebClient).DownloadFile($zipUrl, "$env:APPVEYOR_BUILD_FOLDER\$zipName")

Write-Host "Extracting CodeContracts to $codeContractsDir..."
Add-Type -As System.IO.Compression.FileSystem
$zipFile = Get-Item "$env:APPVEYOR_BUILD_FOLDER\$zipName"
[IO.Compression.ZipFile]::ExtractToDirectory($zipFile, $codeContractsDir)

Write-Host "Copying CodeContract targets for MSBuild v12.0..."
Copy-Item "$codeContractsDir\Targets\v12.0\CodeContractsAfter.targets" "$msBuildDir\12.0\Microsoft.Common.Targets\ImportAfter"