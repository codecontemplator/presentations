function Add($a, $b) { return $a + $b }
function Subtract($a, $b) { return $a - $b }

# <-

Describe 'Calculator' {
    It 'Should Add Numbers' {
        Add 2 1 | Should Be 3
    }
    It 'Should Subtract Numbers' {
        Subtract 2 1 | Should Be 1
    }    
}
					
# ->
