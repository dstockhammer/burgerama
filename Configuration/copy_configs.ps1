$configSrcDir = "Configuration"
$configDstDir = "Services"

$configFiles = @("Auth0", "Logging", "MongoDb", "RabbitMq")
$solutions =  @("Venues", "Outings", "OutingScheduler", "Ratings", "Voting")
$projects = @("Api", "App", "Endpoint", "Tests")

$currentDir = Split-Path (Get-Variable MyInvocation).Value.MyCommand.Path

Write-Host "Updating variables in config files..."
foreach ($configFile in $configFiles) {
    [xml]$xml = Get-Content "$currentDir\$configSrcDir\$configFile.template.config"
    
    switch ($configFile) {
        "Auth0" {
            $xml.auth0.issuer = $variables.AUTH0_ISSUER
            $xml.auth0.audience = $variables.AUTH0_AUDIENCE
            $xml.auth0.secret = $variables.AUTH0_SECRET
        }
        
        "Logging" {
            $xml.logging.useConsole = $variables.LOGGING_USE_CONSOLE
            $xml.logging.useLogentries = $variables.LOGGING_USE_LOGENTRIES
            $xml.logging.logentriesKey = $variables.LOGGING_LOGENTRIES_KEY
            $xml.logging.useSeq = $variables.LOGGING_USE_SEQ
            $xml.logging.seqUrl = $variables.LOGGING_SEQ_URL
        }
                
        "MongoDb" {
            $xml.mongoDb.database = $variables.MONGODB_DATABASE
            $xml.mongoDb.connectionString = $variables.MONGODB_CONN_STR
        }
        
        "RabbitMq" {
            $xml.rabbitMq.server = $variables.RABBITMQ_SERVER
            $xml.rabbitMq.vHost = $variables.RABBITMQ_VHOST
            $xml.rabbitMq.userName = $variables.RABBITMQ_USERNAME
            $xml.rabbitMq.password = $variables.RABBITMQ_PASSWORD
        }
        
        default {
            Write-Host "ERROR: Unknown config item found!"
        }
    }
    
    $xml.Save("$currentDir\$configSrcDir\$configFile.confidential.config")
}


Write-Host "Copying config files into appropriate directories..."
foreach ($solution in $solutions) {
    $currentSolutionDir = "$currentDir\$configDstDir\$solution"

    if (Test-Path -path $currentSolutionDir) {
        foreach ($project in $projects) {
            $currentProjectDir = "$currentSolutionDir\$project"
            $currentConfigDir = "$currentProjectDir\Config"

            if ((Test-Path -path $currentProjectDir) -and (Test-Path -path $currentConfigDir)) {
                Write-Host "Copying config files to $currentProjectDir"
                Copy-Item "$currentDir\$configSrcDir\*.confidential.config" "$currentConfigDir"
            }
        }
    }
}