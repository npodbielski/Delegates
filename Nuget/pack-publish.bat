"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild" c:\OneDrive\Delegates\Delegates.sln /t:rebuild /p:configuration=Release
nuget pack Nuget.nuspec -symbols -properties Configuration=Release -outputdirectory packages
nuget push .\packages\DelegatesFactory.1.1.0.nupkg -Source https://www.nuget.org/api/v2/package
nuget push .\packages\DelegatesFactory.1.1.0.symbols.nupkg -source https://nuget.smbsrc.net/