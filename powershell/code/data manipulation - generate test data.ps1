
$template = @"
{
   "Name" : "[person]",
   "Address" : {
        "Street":"[address]",
        "State":"[state abbr]"
    }
}
"@

Invoke-Generate $Template -Count 3 | 
    ForEach-Object { ConvertFrom-Json $_ }
					
