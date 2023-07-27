using Google.OrTools.Sat;

// Src: http://forum.enjoysudoku.com/patterns-game-results-t6291.html
var sudokuRaw = "010020300004005060070000008006900070000100002030048000500006040000800106008000000";

CpModel model = new CpModel();

var cells = new List<Cell>();
int i = 0;
for(int row=0; row<9; row++)
{
    for(int col=0; col<9; col++)
    {
        var cell = new Cell();
        cell.col = col;
        cell.row = row;
        cell.block = row / 3 * 3 + col / 3;
        var ch = sudokuRaw[i++];
        if (ch == '0')
        {
            cell.value = model.NewIntVar(1, 9, i.ToString());
        }
        else
        {
            var val = ch - '0';
            cell.value = model.NewConstant(val);
        }
        cells.Add(cell);
    }
}

for(int row=0; row<9; row++)
{
    var values = cells.Where(cell => cell.row == row).Select(cell => cell.value).ToArray();
    model.AddAllDifferent(values);
}

for (int col = 0; col < 9; col++)
{
    var values = cells.Where(cell => cell.col == col).Select(cell => cell.value).ToArray();
    model.AddAllDifferent(values);
}

for (int block = 0; block < 9; block++)
{
    var values = cells.Where(cell => cell.block == block).Select(cell => cell.value).ToArray();
    model.AddAllDifferent(values);
}

CpSolver solver = new CpSolver();
CpSolverStatus status = solver.Solve(model);

i = 0;
for (int row = 0; row < 9; row++)
{
    for (int col = 0; col < 9; col++)
    {
        var cell = cells[i++];
        var value = solver.Value(cell.value);
        Console.Write(value);
    }
    Console.WriteLine();
}

class Cell
{
    public int col;
    public int row;
    public int block;
    public IntVar value;
}
