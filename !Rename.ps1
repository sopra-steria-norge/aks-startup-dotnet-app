# Kjør som Administrator i forkant:
# Set-ExecutionPolicy RemoteSigned

$Source="AksStartupDotnetApp"
$Target="AksStartupDotnetApp"

Get-ChildItem -Filter "*" -Recurse | ForEach {  (Get-Content $_.PSPath | ForEach {$_ -creplace $Source, $Target}) | Set-Content $_.PSPath }
   
Get-ChildItem -Filter "*" -Recurse | Rename-Item -NewName {$_.name -creplace $Source, $Target }
