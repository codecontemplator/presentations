
# Enforce tls12
[Net.ServicePointManager]::SecurityProtocol = 
    [Net.SecurityProtocolType]::Tls12

# Create json body
$body = @{ term1=10; term2=20 } | ConvertTo-Json

# Make request
Invoke-RestMethod `
 -Uri https://func-kompetenslunchps-sample.azurewebsites.net/api/add `
 -Method Post `
 -Body $body
					
