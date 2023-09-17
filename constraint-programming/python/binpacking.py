from ortools.sat.python import cp_model
from typing import NamedTuple

class Truck(NamedTuple):
    capacity: int

class Box(NamedTuple):
    weight: int

trucks = [Truck(8000), Truck(5000), Truck(3000)]
boxes = [Box(4000), Box(3000), Box(3000), Box(2000)]

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
    model.Add(
        sum(
            [ boxInTruck[(ti,bi)] * boxes[bi].weight for bi in range(len(boxes)) ]
        ) <= trucks[ti].capacity * truckInUse[ti])

# if a truck is not used it must not contains any boxes
#for ti in range(len(trucks)):
#    for bi in range(len(boxes)):
#        v = boxInTruck[(ti,bi)]
#        model.Add(v == 0).OnlyEnforceIf(truckInUse[ti].Not())

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