#! /bin/pwsh

Remove-Item -r "*/bin" 
Remove-Item -r "*/obj"
dotnet build -c Release
./.paket/paket.exe pack "nuget"
