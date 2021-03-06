#REQUIRES -Version 3.0

<#  
.SYNOPSIS  
	Adds a powershell script file to profile.
	
.DESCRIPTION  
    
.PARAMETER Path
	Path to powershell script which shoould be ran with profile

.PARAMETER ProfileFile
	Profile file. By default this is the $profile

.PARAMETER Id
	A unique ID which identifies the file in boot list.

.NOTES  
    Author         : Marcin Dembowski [myname.mysurname(at)gmail.com]
    Website        : http://marcindembowski.wordpress.com/
    Prerequisite   : PowerShell V3
    License        : Use it as you wish. It would be nice if you will mention this script in your work ;)

.LINK  
    http://marcindembowski.wordpress.com/
	https://github.com/D3M80L/SharpProject/tree/master/Powershell

.EXAMPLE  
	PS> Add-ToProfile .\SetPrompt.ps1
	
	ID            Enabled  ScriptFile           CreatedOn
	--            -------  ----------           ---------
	SetPrompt.ps1 True     H:\marcin\Scripts... 2013-08-19T19:09:32.8483541Z
	
	Adds the SetPrompt.ps1 file to a boot list. 
#>

function Add-ToProfile
{
	param
	(
        [string]
        $Path,

        [string]
		$ProfileFile = $profile,
		
		[string]
		$Id = $null
	)

    $Path = [System.IO.Path]::Combine($pwd, $Path);
    $Path = [System.IO.Path]::GetFullPath($Path);

    if (-not (Test-Path $Path))
    {
        Write-Error "The script file '$Path' which should be added to profile does not exsits."
        return
    }

    if (-not (Test-Path $ProfileFile))
    {
        Write-Warning "Creating profile file '$ProfileFile'"
        "" | Out-File -Encoding Utf8 -FilePath $ProfileFile
    }

    $bootFolderPath = "$ProfileFile.boot"
    if (-not (Test-Path $bootFolderPath))
    {
        mkdir $bootFolderPath 2>&1 | Out-Null

        if (-not (Test-Path $bootFolderPath)) 
        {
            Write-Error "Could not create a boot folder '$bootFolderPath'"
            return
        }
    }

    $bootFilePath = Join-Path $bootFolderPath 'boot.dat'
    if (-not (Test-Path $bootFilePath) -or ((Import-Csv $bootFilePath) -eq $null))
    {
        "ID;Enabled;ScriptFile;CreatedOn" | Out-File -Encoding UTF8 -FilePath $bootFilePath
    }

	if ([string]::IsNullOrEmpty($Id)) {
    	$Id = Split-Path $Path -Leaf
	}
	
	$existingItem = Get-ProfileScripts | ?{ $_.ScriptFile -eq $Path -or $_.ID -eq $id } | Select -First 1
	if ($existingItem -ne $null)
	{
		Write-Error "There is still an item with the same ID or Path '($existingItem)'"
		return
	}
	
    "$id;$true;$Path;$([datetime]::UtcNow.ToString('o'))" | Out-File -Encoding UTF8 -FilePath $bootFilePath -Append

    if (-not $(cat $ProfileFile | Select-String '#REGION PROFILELOADER' -Quiet))
    {
'#REGION PROFILELOADER
#This region was added by Add-ToProfile. Do not modify it.
Get-ProfileScripts $profile -ValidOnly | %{
	. $_.ScriptFile
}
#ENDREGION PROFILELOADER
' 		| Out-File -Encoding utf8 -Append -FilePath $ProfileFile
    }
	
	Get-ProfileScripts | ?{ $_.ID -eq $Id }
}

<#  
.SYNOPSIS  
	Removes the script from profile boot list.
	
.DESCRIPTION  
    
.PARAMETER Id
	The ID of the script from boot file.

.PARAMETER ProfileFile
	Profile file. By default this is the $profile

.NOTES  
    Author         : Marcin Dembowski [myname.mysurname(at)gmail.com]
    Website        : http://marcindembowski.wordpress.com/
    Prerequisite   : PowerShell V3
    License        : Use it as you wish. It would be nice if you will mention this script in your work ;)

.LINK  
    http://marcindembowski.wordpress.com/
	https://github.com/D3M80L/SharpProject/tree/master/Powershell

.EXAMPLE  
	PS> Remove-FromProfile -Id SetPrompt.ps1
	
	ID            Enabled  ScriptFile           CreatedOn
	--            -------  ----------           ---------
	SetPrompt.ps1 True     H:\marcin\Scripts... 2013-08-19T19:09:32.8483541Z
	
	Removes the SetPrompt.ps1 file to a boot list.  Displays items which were removed.
#>
function Remove-FromProfile
{
	param
	(
        [string]
        $ProfileFile = $profile,
		
		[string]
		$Id = $null
	)
	
	if (-not (Test-Path $ProfileFile))
    {
        Write-Warning "Profile file '$ProfileFile' does not exist."
		return
    }
	
	$bootFolderPath = "$ProfileFile.boot"
	$bootFilePath = Join-Path $bootFolderPath 'boot.dat'
	
	if (-not (Test-Path $bootFilePath))
    {
        Write-Warning "There is no boot '$bootFilePath'. Nothing to remove."
		return
    }
	
	$removeItems = Get-ProfileScripts | ?{ $_.ID -eq $Id }
	if ($removeItems -eq $null)
	{
		Write-Warning "There are no scripts in boot file with the '$Id' ID."
		return
	}
	
	$newBootList = Get-ProfileScripts | ?{ $_.ID -ne $Id } 
	$newBootList | Export-Csv -Encoding utf8 -Delimiter ';' -Path $bootFilePath
	
	# Display the removed items
	$removeItems
}

<#  
.SYNOPSIS  
	Returns a list of scripts which may be loaded during boot.
	
.DESCRIPTION  
    
.PARAMETER ProfileFile
	Profile file. By default this is the $profile
	
.PARAMETER ValidOnly
	Display only valid files - enabled and those which exists physically.
	
.NOTES  
    Author         : Marcin Dembowski [myname.mysurname(at)gmail.com]
    Website        : http://marcindembowski.wordpress.com/
    Prerequisite   : PowerShell V3
    License        : Use it as you wish. It would be nice if you will mention this script in your work ;)

.LINK  
    http://marcindembowski.wordpress.com/
	https://github.com/D3M80L/SharpProject/tree/master/Powershell

.EXAMPLE  
	PS> Get-ProfileScripts
	
	ID            Enabled  ScriptFile           CreatedOn
	--            -------  ----------           ---------
	SetPrompt.ps1 True     H:\marcin\Scripts... 2013-08-19T19:09:32.8483541Z
	Invalid.ps1   True     H:\marcin\Scripts... 2013-08-19T19:10:42.5321672Z
	Disabled.ps1  False    H:\marcin\Scripts... 2013-08-19T19:11:43.3123491Z
	
	Returns a list of scripts which may be loaded with current profile.
	
.EXAMPLE  
	PS> Get-ProfileScripts -ValidOnly
	
	ID            Enabled  ScriptFile           CreatedOn
	--            -------  ----------           ---------
	SetPrompt.ps1 True     H:\marcin\Scripts... 2013-08-19T19:09:32.8483541Z

	Returns a list of scripts which are enabled and which exists physically uder defined ScriptFile location.
#>
function Get-ProfileScripts
{
	param
    (
        [string]
		$ProfileFile = $profile,
		
		[switch]
		$ValidOnly
    )
	
	if (-not (Test-Path $ProfileFile))
    {
        Write-Warning "Profile file '$ProfileFile' does not exists."
        return
    }
	
	$bootFolderPath = "$ProfileFile.boot"
    if (-not (Test-Path $bootFolderPath))
    {
        Write-Warning "Boot folder '$bootFolderPath' does not exists"
        return;
    }
	
	$bootFilePath = Join-Path $bootFolderPath 'boot.dat'
    if (-not (Test-Path $bootFilePath))
    {
        Write-Warning "Boot file '$bootFilePath' does not exists"
        return
    }

	Import-Csv -Encoding UTF8 -Path $bootFilePath -Delimiter ';' | ?{ -not $ValidOnly -or ($_.Enabled -eq 'True') -and (Test-Path $_.ScriptFile) }
}

#REGION MODULEEXPORTS
#Export-ModuleMember Add-ToProfile
#Export-ModuleMember Get-ProfileScripts
#Export-ModuleMember Remove-FromProfile
#ENDREGION