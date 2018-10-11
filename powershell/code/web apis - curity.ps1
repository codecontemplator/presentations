
$token = Get-CurityToken clientid scope1, scope2

Invoke-RestMethod ... -Headers @{ Authorization = "Bearer $token" }
					
