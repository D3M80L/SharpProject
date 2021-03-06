#REQUIRES -Version 3.0

<#  
.SYNOPSIS  
    Renames a .ps1 file to a module .psm1 file and imports to a module folder.

.DESCRIPTION  
    Makes a copy of a provided .ps1 file to a .psm1 file and by default stores it to the first module folder from env:PSModulePath.
    Inside a module folder a folder with module name is created and the .psm1 is copied.

	Any commented Export-ModuleMember will be uncommented in the exported file.
	
.PARAMETER Path
	The path to powershell script which should be imported as module.
	
.PARAMETER Force
	When a module with the same name already exists in the module path, then this switch allows to overwrite the module script.
	
.PARAMETER ModulePath
	Path to folder where modules are stored. 
	When the path is joined with ';' char, then the first splitted part will be taken - see examples.
	BY default this is the value of $env:PSModulePath
	
.PARAMETER As
	Import the script as module with another name.

.NOTES  
    Author         : Marcin Dembowski [myname.mysurname(at)gmail.com]
    Website        : http://marcindembowski.wordpress.com/
    Prerequisite   : PowerShell V3
    License        : Use it as you wish. It would be nice if you will mention this script in your work ;)

.LINK  
    http://marcindembowski.wordpress.com/
	https://github.com/D3M80L/SharpProject/tree/master/Powershell

.EXAMPLE  
    Import-AsModule .\MyScript.ps1

    Assuming that the $env:PSModulePath points to three locations
    C:\MyModules;C:\Windows\system32\WindowsPowerShell\v1.0\Modules\;C:\Other
    the MyScript.ps1 file will be copied to
    C:\MyModules\MyScript\MyScript.psm1

.EXAMPLE    
    Import-AsModule .\MyScript.ps1 -Force

    When there is already a module with the same name, then the module will be overridden.

.EXAMPLE    
    Import-AsModule .\MyScript.ps1 -As MyModule

    Imports the provided powershell file script to a module under name 'MyModule'
	
.EXAMPLE  
    Import-AsModule .\MyScript.ps1 -ModulePath C:\MyModules

    Imports the script as a module to C:\MyModules module folder.
#>
function Import-AsModule
{
    param
    (
        [Parameter(Mandatory=$True)]
        [string]
        $Path,

        [switch]
        $Force,

        [string]
        $As,
		
		[string]
		$ModulePath = $env:PSModulePath
    )

    # First check if the file which should be used as module exists
    if ((Test-Path $Path) -eq $false)
    {
        Write-Error "The powershell script does not exists or is invalid"
        return; 
    }

    # Check if the module is already installed
    $itemProperty = Get-ItemProperty $Path;
    $moduleName = $itemProperty.BaseName;

    if (![string]::IsNullOrWhiteSpace($As))
    {
        $moduleName = $As;
    }

    $existingModuleInfo = Get-Module -Name $moduleName
    if ($existingModuleInfo -ne $null -and -not $Force) 
    {
        Write-Error "There is already a module with the same name";
        return;
    }

    # copy the script to module path
    $module = $ModulePath.Split(';') | Select -First 1
	
	if (-not (Test-Path $module))
	{
		Write-Error "The module folder '$module' does not exist."
		return;
	}
	
	if (-not ($env:PSModulePath.ToLower().Contains($module.ToLower())))
	{
		Write-Warning "The module folder '$module' is not listed in current '`$env:PSModulePath'"
	}
	
	if ($module -eq $null) 
	{
		Write-Error "There are no module paths defined!";
		return;
	}
	
	if (-not (Test-Path $module)) 
	{
		mkdir $module 2>&1 | Out-Null
		if (-not (Test-Path $module)) 
		{
			Write-Error "Module path '$module' could not be created.";
			return;
		}
		
		Write-Warning "Created folder for modules '$module'"
	}
    
    if ((Test-Path "$module\$moduleName") -and -not $Force) 
    {
        Write-Error "Seems that there is another module with the same name under '$module'";
        return;
    }

    mkdir "$module\$moduleName" 2>&1 | Out-Null
	$content = cat $Path
	$content -replace '^#Export-ModuleMember','Export-ModuleMember' | Out-File -FilePath "$module\$moduleName\$moduleName.psm1"

    Write-Host "Imported as module '$moduleName' under '$module'."
}


#REGION MODULEXPORT
#Export-ModuleMember Import-AsModule
#ENDREGION