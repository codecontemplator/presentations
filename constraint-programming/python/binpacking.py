from ortools.sat.python import cp_model
from typing import NamedTuple

class Truck(NamedTuple):
    capacity: int

class Box(NamedTuple):
    weight: int

trucks = [Truck(3000), Truck(5000), Truck(8000)]
boxes = [Box(3000), Box(3000), Box(4000), Box(2000)]

model = cp_model.CpModel()

boxInTruck = {}
for ti in range(len(trucks)):
    for bi in range(len(boxes)):
        boxInTruck[(ti,bi)] = model.NewBoolVar(f"boxInTruck: ti {ti}, bi {bi}")

truckInUse = []
for ti in range(len(trucks)):
    truckInUse.append(model.NewBoolVar(f"truckInUse: ti {ti}"))

# a box must be in exactly one truck
for bi in range(len(boxes)):
    model.AddExactlyOne([ boxInTruck[(ti,bi)] for ti in range(len(trucks)) ])

# the box weight must not exceed capacity
for ti in range(len(trucks)):
    boxWeight = [ boxInTruck[(ti,bi)] * boxes[bi].weight for bi in range(len(boxes)) ]
    totalWeight = sum(boxWeight)
    model.Add(totalWeight <= trucks[ti].capacity * truckInUse[ti])

model.Minimize(sum(truckInUse))

solver = cp_model.CpSolver()
status = solver.Solve(model)

if status == cp_model.OPTIMAL or status == cp_model.FEASIBLE:
    print("solved")
    for ti in range(len(trucks)):
        contained = [ boxes[bi] for bi in range(len(boxes)) if solver.Value(boxInTruck[(ti,bi)]) == 1]
        print(f"truck {ti} is loaded with {contained}")
else:
    print("not solved")