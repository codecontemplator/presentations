
$config = @"
{
	"connectionString": "some connection",
	"instances": 2
}
"@ | ConvertFrom-Json

$config.connectionString = "some other connection"
$config.instances = 1

$config | ConvertTo-Json | Out-File "appsettings.json"
					
