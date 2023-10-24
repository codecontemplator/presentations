from ortools.sat.python import cp_model
from typing import NamedTuple

class Truck(NamedTuple):
    capacity: int

class Box(NamedTuple):
    weight: int

trucks = [Truck(3000), Truck(5000), Truck(3000), Truck(3000), Truck(8000)]
boxes = [Box(3000), Box(3000), Box(4000), Box(2000)]

model = cp_model.CpModel()

boxInTruck = {}
for ti in range(len(trucks)):
    for bi in range(len(boxes)):
        boxInTruck[(bi,ti)] = model.NewBoolVar(f"boxInTruck: ti {ti}, bi {bi}")

truckInUse = []
for ti in range(len(trucks)):
    truckInUse.append(model.NewBoolVar(f"truckInUse: ti {ti}"))

# a box must be in exactly one truck
for bi in range(len(boxes)):
    model.AddExactlyOne([ boxInTruck[(bi,ti)] for ti in range(len(trucks)) ])

# the box weight must not exceed capacity
for ti in range(len(trucks)):
    weights = [ boxInTruck[(bi,ti)] * boxes[bi].weight for bi in range(len(boxes)) ]
    model.Add(sum(weights) <= trucks[ti].capacity * truckInUse[ti])

model.Minimize(sum(truckInUse))

solver = cp_model.CpSolver()
status = solver.Solve(model)

if status != cp_model.OPTIMAL and status != cp_model.FEASIBLE:
    print("no solution found")
    exit(-1)

print("solution found")
for ti in range(len(trucks)):
    print(f"truck {trucks[ti]}:")
    for bi in range(len(boxes)):
        if solver.Value(boxInTruck[(bi,ti)]) == 1:
            print(f"  {boxes[bi]}")
