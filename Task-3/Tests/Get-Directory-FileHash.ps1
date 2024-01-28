param (
    [Parameter(Mandatory=$true)]
	[string]$Path,
    [Parameter(Mandatory=$true)]
    [string]$Algorithm
)

Get-ChildItem -Path $Path -Recurse -File | ForEach-Object {
    $hash = Get-FileHash -Path $_.FullName -Algorithm $Algorithm
    Write-Output "$($hash.Hash),$($_.FullName)"
}