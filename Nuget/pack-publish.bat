nuget pack Nuget.nuspec -symbols -properties Configuration=Release -outputdirectory packages
nuget push .\packages\DelegatesFactory.1.0.1.nupkg -Source https://www.nuget.org/api/v2/package
nuget push .\packages\DelegatesFactory.1.0.1.symbols.nupkg -source https://nuget.smbsrc.net/