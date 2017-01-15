nuget pack Nuget.nuspec -symbols -properties Configuration=Release -outputdirectory bin\Release
--nuget push .\bin\Release\DelegateFactory.1.0.0.nupkg -Source https://www.nuget.org/api/v2/package