
# Create a function app
New-AzureRmResource `
    -ResourceType 'Microsoft.Web/Sites' `
    -ResourceName 'func-kompetenslunchps-sample' `
    -ResourceGroupName 'Group-KompetensLunchPS' `
    -Kind 'functionapp' `
    -Location 'West Europe' `
    -Properties @{} `
    -Force

					
