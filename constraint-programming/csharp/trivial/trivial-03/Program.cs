using Google.OrTools.Sat;

// Creates the model.
CpModel model = new CpModel();

// Creates the variables.
int num_vals = 3;

IntVar x = model.NewIntVar(0, num_vals - 1, "x");
IntVar y = model.NewIntVar(0, num_vals - 1, "y");
IntVar z = model.NewIntVar(0, num_vals - 1, "z");

// Creates the constraints.
model.Add(x != y);

// Optmization
model.Maximize(x + y + z);

// Creates a solver and solves the model.
CpSolver solver = new CpSolver();
CpSolverStatus status = solver.Solve(model);

if (status == CpSolverStatus.Optimal || status == CpSolverStatus.Feasible)
{
    Console.WriteLine("x = " + solver.Value(x));
    Console.WriteLine("y = " + solver.Value(y));
    Console.WriteLine("z = " + solver.Value(z));
}
else
{
    Console.WriteLine("No solution found.");
}
