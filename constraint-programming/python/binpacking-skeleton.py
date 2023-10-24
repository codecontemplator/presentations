from ortools.sat.python import cp_model
from typing import NamedTuple

class Truck(NamedTuple):
    capacity: int

class Box(NamedTuple):
    weight: int

trucks = [Truck(3000), Truck(5000), Truck(3000), Truck(3000), Truck(8000)]
boxes = [Box(3000), Box(3000), Box(4000), Box(2000)]

model = cp_model.CpModel()

# create variable matrix: box 'bi' in truck 'ti'?

# a box must be in exactly one truck

# the box weight must not exceed capacity

# bool variables for trucks in use

# minimize trucks in use

solver = cp_model.CpSolver()
status = solver.Solve(model)

if status != cp_model.OPTIMAL and status != cp_model.FEASIBLE:
    print("no solution found")
    exit(-1)

print("solution found")
