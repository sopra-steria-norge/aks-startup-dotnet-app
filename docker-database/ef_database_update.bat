cd %~dp0

cd ..
cd src\AksStartupDotnetApp

dotnet tool restore

dotnet ef database update --verbose --connection "Server=localhost,9200;Database=aks-startup-dotnet-app;User Id=sa;Password=Secret1234;"

pause
