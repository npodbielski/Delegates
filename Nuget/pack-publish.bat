nuget pack Nuget.nuspec -symbols -properties Configuration=Release -outputdirectory packages
nuget push .\packages\DelegatesFactory.1.0.0.nupkg -Source https://www.nuget.org/api/v2/package