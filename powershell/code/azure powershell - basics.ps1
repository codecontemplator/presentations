
# Login						
Login-AzureRmAccount -Subscription 'Collector - Solutions - Dev'

# Create a resource group
New-AzureRmResourceGroup `
  -Name 'Group-KompetensLunchPS' `
  -Location 'West Europe'

# Create a web app
New-AzureRmWebApp `
  -Name 'web-kompetenslunchps-sample' `
  -Location 'West Europe' `
  -ResourceGroupName 'Group-KompetensLunchPS'
					
