$numberOfCommits = & 'C:\Program Files\git\bin\git.exe' rev-list origin/master..HEAD --count
$branch = git rev-parse --abbrev-ref HEAD
if ($branch -eq 'develop'){
	$semver = '-unstable'
}
else{
	$semver = ''
}
$content = Get-Content 'C:\Program Files (x86)\Jenkins\workspace\Delegates_Develop\src\Common\Common.props'
#replace version
$content = $content -replace "(.*\<version.*)\.(\d*)\<",('$1.'+$numberOfCommits+$semver+'<')
#replace assembly version and file version
$content = $content -replace "(.*(Assembly|File)Version.*)\.(\d*)",('$1.'+$numberOfCommits)
#replace copyright date
$content = $content -replace "(.*Copyright © npodbielski)\s(\d*)",('$1 '+(Get-Date).Year)
Set-Content 'C:\Program Files (x86)\Jenkins\workspace\Delegates_Develop\src\Common\Common.props' $content

$content = Get-Content 'C:\Program Files (x86)\Jenkins\workspace\Delegates_Develop\src\Common\SolutionAssemblyInfo.cs'
#replace assembly version and file version
$content = $content -replace "(.*Version.*)\.(\d*)",('$1.'+$numberOfCommits)
#replace copyright date
$content = $content -replace "(.*Copyright © npodbielski)\s(\d*)",('$1 '+(Get-Date).Year)
Set-Content 'C:\Program Files (x86)\Jenkins\workspace\Delegates_Develop\src\Common\SolutionAssemblyInfo.cs' $content

$content = Get-Content 'C:\Program Files (x86)\Jenkins\workspace\Delegates_Develop\Nuget\Nuget.nuspec'
#replace version
$content = $content -replace "(.*\<version.*)\.(\d*)\<",('$1.'+$numberOfCommits+$semver+'<')
#replace copyright date
$content = $content -replace "(.*Copyright © npodbielski)\s(\d*)",('$1 '+(Get-Date).Year)
Set-Content 'C:\Program Files (x86)\Jenkins\workspace\Delegates_Develop\Nuget\Nuget.nuspec' $content