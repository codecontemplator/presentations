
# Create a function app
New-AzureRmResource `
    -ResourceType 'Microsoft.Web/Sites' `
    -Kind 'functionapp' `
    -ResourceName 'func-kompetenslunchps-sample' `
    -ResourceGroupName 'Group-KompetensLunchPS' `
    -Location 'West Europe' `
    -Properties @{} `
    -Force
					
