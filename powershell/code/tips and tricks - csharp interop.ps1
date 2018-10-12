
Add-Type @"
public class Calculator
{
    public int Add(int a, int b) { return a + b; }
    public int Subtract(int a, int b) { return a - b; }
}
"@

$calculator = New-Object Calculator
$calculator.Add(2, 1)
$calculator.Subtract(2, 1)
					
