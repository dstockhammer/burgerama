$configSrcDir = "Configuration"
$configDstDirs = @("Services", "Web")

$configFiles = @("Auth0", "Logging", "MongoDb", "RabbitMq")
$solutions =  @("Venues", "Outings", "OutingScheduler", "Ratings", "Voting")
$projects = @("Api", "App", "Endpoint", "Tests")

Write-Host "Updating variables in config files..."
foreach ($configFile in $configFiles) {
    [xml]$xml = Get-Content "$env:APPVEYOR_BUILD_FOLDER\$configSrcDir\$configFile.template.config"
    
    switch ($configFile) {
        "Auth0" {
            $xml.auth0.issuer = $env:AUTH0_ISSUER
            $xml.auth0.audience = $env:AUTH0_AUDIENCE
            $xml.auth0.secret = $env:AUTH0_SECRET
        }
        
        "Logging" {
            $xml.logging.useConsole = $env:LOGGING_USE_CONSOLE
            $xml.logging.useLogentries = $env:LOGGING_USE_LOGENTRIES
            $xml.logging.logentriesKey = $env:LOGGING_LOGENTRIES_KEY
        }
                
        "MongoDb" {
            $xml.mongoDb.database = $env:MONGODB_DATABASE
            $xml.mongoDb.connectionString = $env:MONGODB_CONN_STR
        }
        
        "RabbitMq" {
            $xml.rabbitMq.server = $env:RABBITMQ_SERVER
            $xml.rabbitMq.vHost = $env:RABBITMQ_VHOST
            $xml.rabbitMq.userName = $env:RABBITMQ_USERNAME
            $xml.rabbitMq.password = $env:RABBITMQ_PASSWORD
        }
        
        default {
            Write-Host "ERROR: Unknown config item found!"
        }
    }
    
    $xml.Save("$env:APPVEYOR_BUILD_FOLDER\$configSrcDir\$configFile.confidential.config")
}

Write-Host "Copying config files into appropriate directories..."
foreach ($solution in $solutions) {
    $currentSolutionDir = "$env:APPVEYOR_BUILD_FOLDER\$configDstDir\$solution"

    if (Test-Path -path $currentSolutionDir) {
        foreach ($project in $projects) {
            $currentProjectDir = "$currentSolutionDir\$project"
            $currentConfigDir = "$currentProjectDir\Config"

            if ((Test-Path -path $currentProjectDir) -and (Test-Path -path $currentConfigDir)) {
                Write-Host "Copying config files to $currentProjectDir"
                Copy-Item "$env:APPVEYOR_BUILD_FOLDER\$configSrcDir\*.confidential.config" "$currentConfigDir"
            }
        }
    }
}

# special treatment for Burgerama.Web.Maintenance
$webConfigDir = "$env:APPVEYOR_BUILD_FOLDER\Web\Maintenance\Config"
Write-Host "Copying config files to $currentProjectDir"
Copy-Item "$env:APPVEYOR_BUILD_FOLDER\$configSrcDir\*.confidential.config" "$webConfigDir"