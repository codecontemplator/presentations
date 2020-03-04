$snippetPath = Join-Path ([Environment]::GetFolderPath("MyDocuments")) "Visual Studio 2019\Code Snippets\Visual C#\My Code Snippets"
Copy-Item -Path "$PSScriptRoot\*.snippet" -Destination $snippetPath