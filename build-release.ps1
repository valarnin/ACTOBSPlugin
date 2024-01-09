$IncludeFiles = "ACTOBSPlugin.dll","Newtonsoft.Json.dll","System.Reactive.dll","System.Threading.Channels.dll","System.Threading.Tasks.Extensions.dll","Websocket.Client.dll","obs-websocket-dotnet.dll"

dotnet publish -c Release

rm -r bin\release-archive -ErrorAction SilentlyContinue > $null

mkdir bin\release-archive\ACTOBSPlugin -ea 0 -Force > $null

foreach ($file in $IncludeFiles) {
    cp bin\Release\$file bin\release-archive\ACTOBSPlugin\$file
}

Compress-Archive -Path "bin\release-archive\ACTOBSPlugin" -DestinationPath bin\release-archive\ACTOBSPlugin.zip