
task default -depends Test

task Test -depends Compile, Clean {
  "Testing"
}

task Compile -depends Clean {
  "Compiling"
}

task Clean {
  "Cleaning"
}
					
