Solution solution = null;

for(int x=0; x<=2; x++)
{
    for(int y=0; y<=2; y++)
    {
        for(int z =0; z<=2; z++)
        {
            if (x != y)
            {
                int objective = x + y + z;
                if (solution == null || objective > solution.objective)
                {
                    solution = new Solution(x, y, z, objective);
                }
            }
        }
    }
}

if (solution == null)
{
    Console.WriteLine("No solution found");
}
else
{
    Console.WriteLine("Solution: " + solution);
}

record Solution(int x, int y, int z, int objective);
