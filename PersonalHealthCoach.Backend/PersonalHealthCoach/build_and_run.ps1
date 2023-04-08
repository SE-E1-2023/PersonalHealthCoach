$currentDirectory = Get-Location

$functionsPath = Join-Path $currentDirectory "Presentation\HealthCoach.Functions.Isolated\HealthCoach.Functions.Isolated.csproj"
$sharedCorePath = Join-Path $currentDirectory "Shared\HealthCoach.Shared.Core\HealthCoach.Shared.Core.csproj"
$sharedInfrastructurePath = Join-Path $currentDirectory "Shared\HealthCoach.Shared.Infrastructure\HealthCoach.Shared.Infrastructure.csproj"
$sharedWebPath = Join-Path $currentDirectory "Shared\HealthCoach.Shared.Web\HealthCoach.Shared.Web.csproj"
$infrastructurePath = Join-Path $currentDirectory "Infrastructure\HealthCoach.Infrastructure\HealthCoach.Infrastructure.csproj"
$domainPath = Join-Path $currentDirectory "Core\HealthCoach.Core.Domain\HealthCoach.Core.Domain.csproj"
$businessPath = Join-Path $currentDirectory "Core\HealthCoach.Core.Business\HealthCoach.Core.Business.csproj"

# 1. Check if the PostgreSQL server service is running
$serviceName = "postgresql-x64-15" # Replace with your PostgreSQL service name
$serviceStatus = Get-Service -Name $serviceName -ErrorAction SilentlyContinue | Select-Object -ExpandProperty Status

if ($serviceStatus -eq "Running") {
    Write-Host "PostgreSQL server service is running."
} else {
    Write-Host "PostgreSQL server service is not running. Please start the service before proceeding."
    exit
}

# 2. Run the database migrations
dotnet ef database update --startup-project $functionsPath --project $sharedInfrastructurePath

# 3. Build the project
$buildDirectory = Join-Path $currentDirectory "app\build" # Replace with your build directory
$publishDirectory = Join-Path $currentDirectory "app\publish" # Replace with your publish directory

dotnet build $functionsPath -c Release -o $buildDirectory
dotnet publish $functionsPath -c Release -o $publishDirectory

dotnet build $sharedCorePath -c Release -o $buildDirectory
dotnet publish $sharedCorePath -c Release -o $publishDirectory

dotnet build $sharedInfrastructurePath -c Release -o $buildDirectory
dotnet publish $sharedInfrastructurePath -c Release -o $publishDirectory

dotnet build $sharedWebPath -c Release -o $buildDirectory
dotnet publish $sharedWebPath -c Release -o $publishDirectory

dotnet build $infrastructurePath -c Release -o $buildDirectory
dotnet publish $infrastructurePath -c Release -o $publishDirectory

dotnet build $domainPath -c Release -o $buildDirectory
dotnet publish $domainPath -c Release -o $publishDirectory

dotnet build $businessPath -c Release -o $buildDirectory
dotnet publish $businessPath -c Release -o $publishDirectory

# Copy the appsettings.json, local.settings.json, host.json file to the publish directory
$functionsDirectory = Join-Path $currentDirectory "Presentation\HealthCoach.Functions.Isolated"
Copy-Item -Path $functionsDirectory\appsettings.json -Destination $publishDirectory -Force
Copy-Item -Path $functionsDirectory\local.settings.json -Destination $publishDirectory -Force
Copy-Item -Path $functionsDirectory\host.json -Destination $publishDirectory -Force

if ($LASTEXITCODE -eq 0) {
    Write-Host "Project build succeeded."
} else {
    Write-Host "Project build failed. Please fix build errors before proceeding."
    exit
}

# 4. Run the project
Push-Location -Path $publishDirectory # Set the working directory to the project directory

# run the dotnet project from the publish directory
# dotnet HealthCoach.Functions.Isolated.dll --environment Development
func start

Pop-Location # Restore the previous working directory