#REQUIRES -Version 3.0

<#  
.SYNOPSIS  
    Allows to remember locations by defining them under a user-defined label.

.DESCRIPTION  
    
.PARAMETER Label
	Name of label which should be used to identify the stored location.

.PARAMETER Remember
	Switch informing that the location should be stored under provided label.

.PARAMETER Delete
	Delete the stored label.

.PARAMETER Info
	Get information about stored label.

.PARAMETER Location
	By default this is the current working directory ($pwd).
	Information which should be stored.

.PARAMETER Confirm
	Confirm label delete without asking the user.

.PARAMETER withList
    Display the content of stored location after navigating.

.NOTES  
    Author         : Marcin Dembowski [myname.mysurname(at)gmail.com]
    Website        : http://marcindembowski.wordpress.com/
    Prerequisite   : PowerShell V3
    License        : Use it as you wish. It would be nice if you will mention this script in your work ;)

.LINK  
    http://marcindembowski.wordpress.com/
	https://github.com/D3M80L/SharpProject/tree/master/Powershell
	http://poshcode.org/4398
	[PL] http://marcindembowski.wordpress.com/2013/08/16/pl-powershell-tworzenie-zakladek-dla-lokalizacji/

.EXAMPLE  
    PS> Go-ToLocation


    Label          Location             CreatedOn
    -------------- -------------------- ----------------------------
    projects       H:\marcin\projekty   2013-08-12T19:20:16.5052452Z


    Lists all defined labels, their location and information when the label was created in UTC time.

.EXAMPLE  
    PS> Go-ToLocation projects

    Navigates to location defined under the label 'projects'.

.EXAMPLE
    H:\projects> Go-ToLocation -Remember

    Label          Location             CreatedOn                    WithList
    -------------- -------------------- ---------------------------- ---------
    projects       H:\projects          2013-08-12T19:30:14.3041231Z False

    Remembers the current location using current folder name as label name.
    In this case the label will be 'projects'

.EXAMPLE
    H:\projects> Go-ToLocation -Remember -withList

    Label          Location             CreatedOn                    WithList
    -------------- -------------------- ---------------------------- ---------
    projects       H:\projects          2013-08-12T19:30:14.3041231Z True

    Remembers the current location using current folder name as label name and sets the list flag to true.
    In this case the label will be 'projects' and after navigating to location with label, the content will be displayed.

.EXAMPLE
    PS> Go-ToLocation -Remember mylabel

    Remembers the current location under label 'mylabel'

.EXAMPLE  
    PS> Go-ToLocation projects -Delete

    Removes the location remembered under 'projects' label.

.EXAMPLE
    PS> Go-ToLocation projects -Info

    Label          Location             CreatedOn
    -------------- -------------------- ----------------------------
    projects       H:\projects          2013-08-12T19:30:14.3041231Z

	Returns information about stored label if exists.
	
.EXAMPLE
    PS> Go-ToLocation -Location c:\user\myname -Remember home

    Label          Location             CreatedOn
    -------------- -------------------- ----------------------------
    myname         C:\user\myname       2013-08-12T19:15:13.4051132Z

    Remembers a location provided by parameter under a specified label.

.EXAMPLE
    PS> Go-ToLocation projects -withList

    Navigates to location defined under the label 'projects' and lists content of the location after navigation.

#>

function Go-ToLocation
{
	param
	(
		[string]
		$Label,
		
		[switch]
		$Remember,
		
		[switch]
		$Delete,

        [switch]
        $Info,

        [string]
        $Location = $pwd,

        [switch]
        $Confirm,

        [switch]
        $WithList
	)
	
	$labelsPath = "$env:APPDATA\Go-ToLocation";
	if ((Test-Path $labelsPath) -ne $true) 
	{
		(mkdir $labelsPath) 2>&1 | Out-Null
	}
	
	$labelsFile = "$labelsPath\labels.dat"
	if (-not (Test-Path $labelsFile))
	{
		"Label;Location;CreatedOn;List" | Out-File -FilePath $labelsFile -Encoding UTF8
	}
	
	if (-not (Test-Path $labelsFile))
	{
		Write-Error "Cannot process, because the location file '$labelsFile' could not be created."
		return
	}

	if ($Remember) 
	{
        if ([string]::IsNullOrWhiteSpace($Label))
        {
            $Label = Split-Path -leaf -path (Get-Location)
        }

		$existingItem = Import-Csv -Delimiter ';' $labelsFile | ? { $_.Label -eq $Label } | Select-Object Location -First 1;
		
		if ($existingItem -ne $null) 
		{
			Write-Error "There is already something under '$Label' which points to: '$($existingItem.Location)'"
			return;
		}
		
		"$Label;$Location;$([datetime]::UtcNow.ToString('o'));$WithList" | Out-File -Append -FilePath $labelsFile -Encoding UTF8
		$Info = $true
	}
	
	if ($Delete) 
	{
        if ([string]::IsNullOrWhiteSpace($Label))
        {
            $Label = Split-Path -leaf -path (Get-Location)
        }

        $existingItem = Import-Csv -Delimiter ';' $labelsFile | ? { $_.Label -eq $Label } | Select-Object Location -First 1;
        if ($existingItem -eq $null)
        {
            return;
        }

        if (-not $Confirm) 
        {
            $yes = New-Object System.Management.Automation.Host.ChoiceDescription "&Yes", "Deletes the label '$Label' from list."
            $no = New-Object System.Management.Automation.Host.ChoiceDescription "&No", "Do not delete the label."
            $options = [System.Management.Automation.Host.ChoiceDescription[]]($yes, $no)
            $result = $host.ui.PromptForChoice("Delete label", "Do you want to delete the label '$Label' which points to '$($existingItem.Location)'?", $options, 1) 
            if ($result -eq 1)
            {
                return;
            }
        }
		Import-Csv -Delimiter ';' $labelsFile -Encoding UTF8 | ? { $_.Label -ne $Label } | Export-Csv -Delimiter ';' "$labelsFile.tmp" -Encoding UTF8
        copy "$labelsFile.tmp" "$labelsFile" -Force
		return;
	}

    if ($Info)
    {
        if ([string]::IsNullOrWhiteSpace($Label))
        {
            $Label = Split-Path -leaf -path (Get-Location)
        }
        Import-Csv -Delimiter ';' $labelsFile -Encoding UTF8 | ? { $_.Label -eq $Label } | Select-Object $_ -First 1;
        return;
    }
	
	if ([string]::IsNullOrWhiteSpace($Label)) 
	{
		# display all labels
		Import-Csv -Delimiter ';' $labelsFile;
		
		return;
	}
	
    # navigate to label
	$item = Import-Csv -Delimiter ';' $labelsFile | Where-Object { $_.Label -eq $Label } | Select-Object Location,List -First 1;
	
	if ($item -eq $null)
	{
		Write-Error "The provided item is not available";
		return;
	}

    $location = $item.Location

    if (-not (Test-Path $location)) {
        Write-Error "The remembered location ($location) does not exists.";
        return;
    }
	
	cd $location

    if (($item.List -eq 'True') -or $WithList) {
        ls
    }
}

#REGION MODULEXPORT
Set-Alias -Name go Go-ToLocation -Force
#Export-ModuleMember Go-ToLocation
#Export-ModuleMember -Alias go
#REGION MODULEXPORT