#REQUIRES -Version 3.0

<#  
.SYNOPSIS  
Start IIS Express from a defined location.

.DESCRIPTION  
    
.PARAMETER Label

.NOTES  
    Author         : Marcin Dembowski [myname.mysurname(at)gmail.com]
    Website        : http://marcindembowski.wordpress.com/
    Prerequisite   : PowerShell V3
    License        : Use it as you wish. It would be nice if you will mention this script in your work ;)

.LINK  
    http://marcindembowski.wordpress.com/
	https://github.com/D3M80L/SharpProject/tree/master/Powershell

.EXAMPLE  
   D:\projects\web\site1 > Start-IISEFromLocation
   
   Will start iis-express website under port 8080 taking curren location.
   
.EXAMPLE  
   D:\projects\web\site1 > Start-IISEFromLocation -port 8081
   
   Will start iis-express website under port 8081 taking curren location.

#>

function Start-IISEFromLocation
{
	param
	(
		[string] $path = $pwd,
		[int] $port = 8080
	)
	$iisExpressExe = 'C:\Program Files\IIS Express\iisexpress.exe'
	if (!(Test-Path $iisExpressExe)) 
	{
		$iisExpressExe = 'C:\Program Files (x86)\IIS Express\iisexpress.exe'
		if (!(Test-Path $iisExpressExe)) 
		{
			Write-Error "Could not found iisexpress.exe file";
			return;
		}
	}
	$params = @()
	$params += "/path:$path"
	$params += "/port:$port"

	Write-Host "IISE : $iisExpressExe"
	Write-Host "Path : $path"
	Write-Host "Port : $port"
	&$iisExpressExe $params
}

#REGION MODULEXPORT
Set-Alias -Name sisel Start-IISEFromLocation -Force
#Export-ModuleMember Start-IISEFromLocation
#Export-ModuleMember -Alias sisel
#REGION MODULEXPORT