nuget pack package.nuspec -properties Configuration=Release -outputdirectory .\bin\Release
nuget push DelegateFactory.1.0.0.nupkg -Source https://www.nuget.org/api/v2/package