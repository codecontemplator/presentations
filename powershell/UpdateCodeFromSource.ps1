cd $PSScriptRoot
#$docpath = Resolve-Path "$PSScriptRoot\..\reveal.js\index.html"
#[xml]$doc = Get-Content $docpath

$presentation = Invoke-WebRequest http://localhost:8000/

#"<h2>(.+?)</h2>(.*?)<h4>(.+?)</h4>"
$s = $presentation.RawContent

#$s -match "(?smi)<h2>(.+?)</h2>(.*?)<h4>(.+?)</h4>(.*?)<code.*?>(.*?)</code>"

#$mymatches = Select-string "(?smi)<h2>(.+?)</h2>(.*?)<h4>(.+?)</h4>(.*?)<code.*?>(.*?)</code>" -InputObject $s -AllMatches
<#
foreach($mymatch in $mymatches)
{
    $h2 = $mymatch.Matches[1].Value
    $h4 = $mymatch.Matches[2].Value
    $code = $mymatch.Matches[3].Value
    
}
#>

$xs = [regex]::Matches($s, "(?smi)<h2>(.+?)</h2>(.*?)<h4>(.+?)</h4>(.*?)<code.*?>(.*?)</code>")

cd code
foreach($x in $xs)
{
    $h2 = $x.Groups[1].Value
    $h4 = $x.Groups[3].Value
    $code = $x.Groups[5].Value
    $file = "$h2 - $h4.ps1".ToLower().Replace(":","")
    Write-Host $file
    $code | Out-File $file -Encoding utf8
}



#Foreach { $_.matches[3] } 


<#
$all = @($presentation.AllElements)

$sections = $all | ? { $_.tagName -match "section" }

foreach($section in $sections)
{
    $h2 = $section.getElementsByTagName("h2").innerText
    $h4 = $section.getElementsByTagName("h4").innerText
    Write-Host $h2
    Write-Host $h4
}
#>

<#
$sections = @($presentation.ParsedHtml.body.getElementsByTagName("section"))

function Process-Section($section)
{
    $h2 = $section.getElementsByTagName("h2").innerText
    $h4 = $section.getElementsByTagName("h4").innerText

    Write-Host $h2
    Write-Host $h4
}

foreach($section in $sections) 
{
    $subsections = @($section.getElementsByTagName("section"))
    if ($subsections.length -gt 0) 
    {
        Write-Host "Sub sections"
        foreach($subsection in $subsections)
        {
            Process-Section $subsection
        }
    }
    else
    {
         Write-Host "Leaf sections"
         Process-Section $section
    }
}
#>