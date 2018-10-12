
Add-Type @"
public class Calculator
{
    public int Add(int a, int b) { return a + b; }
    public int Subtract(int a, int b) { return a - b; }
}
"@

$calculator = [Calculator]::new()
$calculator.Add(2, 1)
$calculator.Subtract(2, 1)
					
