
$requestBody = Get-Content $req -Raw | ConvertFrom-Json
$term1 = $requestBody.term1
$term2 = $requestBody.term2

$response = @{
    sum=$term1+$term2
} | ConvertTo-Json

Out-File -Encoding Ascii -FilePath $res -inputObject $response
					
