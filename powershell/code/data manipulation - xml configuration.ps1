
$config = [xml] @"
<appsettings>
    <connectionstring>some connection</connectionstring>
    <instances count="2"></instances>
</appsettings>
"@

$config.appsettings.connectionstring = "some other connection"
$config.appsettings.instances.count = "1"

$config.Save("appsettings.json")
					
