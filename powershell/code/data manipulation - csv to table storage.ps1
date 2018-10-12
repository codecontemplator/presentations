
$ctx = Get-AzureRmStorageAccount -Name "stokomplunch" -ResourceGroupName "Group-kompetenslunchps" | Select -Expand "Context"
$storageTable = Get-AzureStorageTable –Name "SampleTable" –Context $ctx

# <-

$csvData = @"
Id, Name, Company, Age
1, Sven, Collector, 65
2, Pelle, Collector, 45
3, Jessica, SE Banken, 38
"@

$csvData | ConvertFrom-Csv | ForEach-Object {
    Add-StorageTableRow `
        -table $storageTable `
        -rowKey $_.id `
        -partitionKey "unused" `
        -property @{ Name=$_.Name; Company=$_.Company; Age=$_.Age }     
}
					
# ->

Get-AzureStorageTableRowAll -table $storageTable | Format-Table Name, Age, Company
					


