from ortools.sat.python import cp_model
from typing import Any, NamedTuple

sudokuRaw = "010020300004005060070000008006900070000100002030048000500006040000800106008000000"

model = cp_model.CpModel()

class Cell(NamedTuple):
    row: int
    col: int
    block: int
    var: Any

cells = []
for row in range(9):
    for col in range(9):
        i = row*9+col
        ch = sudokuRaw[i]
        if ch == '0':
            var = model.NewIntVar(1, 9, str(i))
        else:
            var = model.NewConstant(int(ch))
        cell = Cell(row, col, row//3*3+col//3,var)
        cells.append(cell)


for row in range(9):
    model.AddAllDifferent([cell.var for cell in cells if cell.row == row])

for col in range(9):
    model.AddAllDifferent([cell.var for cell in cells if cell.col == col])

for block in range(9):
    model.AddAllDifferent([cell.var for cell in cells if cell.block == block])

solver = cp_model.CpSolver()
solver.Solve(model)

for row in range(9):
    for col in range(9):
        i = row*9+col
        cell = cells[i]
        value = solver.Value(cell.var)
        print(value, end="")
    print()

