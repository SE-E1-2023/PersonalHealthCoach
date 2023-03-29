$currentDirectory = Get-Location

$startupProject = Join-Path $currentDirectory "Presentation\HealthCoach.Functions.Isolated\HealthCoach.Functions.Isolated.csproj"
$project = Join-Path $currentDirectory "Shared\HealthCoach.Shared.Infrastructure\HealthCoach.Shared.Infrastructure.csproj"
$migrationName = "NewMigration_" + (Get-Date -Format "yyyyMMdd_HHmmss")

# Ensure the current directory is set to the solution folder
Set-Location -Path "..\"

# Generate migration
dotnet ef migrations add $migrationName --startup-project $startupProject --project $project
