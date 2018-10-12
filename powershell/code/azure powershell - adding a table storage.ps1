
# Create a table storage
$ctx = New-AzureRmStorageAccount `
  -Kind Storage `
  -Name "stokomplunch" `
  -ResourceGroupName 'Group-KompetensLunchPS' `
  -Location 'West Europe' `
  -SkuName Standard_LRS | Select-Object -ExpandProperty Context

New-AzureStorageTable –Name 'SampleTable' –Context $ctx  
					
