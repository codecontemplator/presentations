
# Create a table storage
$ctx = New-AzureRmStorageAccount `
  -ResourceGroupName 'Group-KompetensLunchPS' `
  -Name "stokomplunch" `
  -Location 'West Europe' `
  -SkuName Standard_LRS `
  -Kind Storage | Select-Object -ExpandProperty Context

New-AzureStorageTable –Name 'SampleTable' –Context $ctx  
					
