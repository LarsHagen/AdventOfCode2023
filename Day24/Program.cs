using Microsoft.Z3;
using Day24;

/*List<Line> lines = File.ReadAllLines("example.txt").Select(s => new Line(s)).ToList();
double minX = 7;
double maxX = 27;
double minY = 7;
double maxY = 27;*/

List<Line> lines = File.ReadAllLines("input.txt").Select(s => new Line(s)).ToList();
double minX = 200000000000000;
double maxX = 400000000000000;
double minY = 200000000000000;
double maxY = 400000000000000;

int numIntersections = 0;

for (int i = 0; i < lines.Count - 1; i++)
{
    var line1 = lines[i];
    for (int j = i + 1; j < lines.Count; j++)
    {
        var line2 = lines[j];
        var intersection = Intersect2D(line1, line2);
        
        if (!intersection.intersect)
            continue;
        
        (double x, double y) directionToIntersection1 = (intersection.point.x - line1.P1.x, intersection.point.y - line1.P1.y);
        (double x, double y) directionToIntersection2 = (intersection.point.x - line2.P1.x, intersection.point.y - line2.P1.y);
        
        if (!SameDirection(directionToIntersection1, (line1.Direction.x, line1.Direction.y)))
            continue;
        
        if (!SameDirection(directionToIntersection2, (line2.Direction.x, line2.Direction.y)))
            continue;
        
        if (intersection.point.x < minX || intersection.point.x > maxX)
            continue;
        
        if (intersection.point.y < minY || intersection.point.y > maxY)
            continue;
        
        numIntersections++;
    }
}

Console.WriteLine("Part 1: " + numIntersections);
Console.WriteLine("Part 2: " + Z3Solve(lines));

bool SameDirection((double x, double y) a, (double x, double y) b)
{
    if (a.x < 0 && b.x > 0)
        return false;
    if (a.x > 0 && b.x < 0)
        return false;
    if (a.y < 0 && b.y > 0)
        return false;
    if (a.y > 0 && b.y < 0)
        return false;
    return true;
}

(bool intersect, (double x, double y) point) Intersect2D(Line a, Line b)
{
    var p1 = (a.P1.x, a.P1.y);
    var p2 = (a.P2.x, a.P2.y);
    var p3 = (b.P1.x, b.P1.y);
    var p4 = (b.P2.x, b.P2.y);
    
    (double x, double y) intersection = (0,0);

    var d = (p2.x - p1.x) * (p4.y - p3.y) - (p2.y - p1.y) * (p4.x - p3.x);

    if (d == 0.0)
    {
        return (false, intersection);
    }

    var u = ((p3.x - p1.x) * (p4.y - p3.y) - (p3.y - p1.y) * (p4.x - p3.x)) / d;
    var v = ((p3.x - p1.x) * (p2.y - p1.y) - (p3.y - p1.y) * (p2.x - p1.x)) / d;

    intersection.x = p1.x + u * (p2.x - p1.x);
    intersection.y = p1.y + u * (p2.y - p1.y);

    return (true, intersection);
}

//I gave up on part 2 and looked in the solutions thread on the subreddit for this one.
long Z3Solve(List<Line> lines)
{
    var ctx = new Context();
    var solver = ctx.MkSolver();
 
    // Coordinates of the stone
    var x = ctx.MkIntConst("x");
    var y = ctx.MkIntConst("y");
    var z = ctx.MkIntConst("z");
 
    // Velocity of the stone
    var vx = ctx.MkIntConst("vx");
    var vy = ctx.MkIntConst("vy");
    var vz = ctx.MkIntConst("vz");
 
    // For each iteration, we will add 3 new equations to the solver.
    // We want to find 9 variables (x, y, z, vx, vy, vz, t0, t1, t2) that satisfy all the equations, so a system of 9 equations is enough.
    for (var i = 0; i < 3; i++)
    {
        var t = ctx.MkIntConst($"t{i}"); // time for the stone to reach the hail
        var line = lines[i];
 
        var px = ctx.MkInt(Convert.ToInt64(line.P1.x));
        var py = ctx.MkInt(Convert.ToInt64(line.P1.y));
        var pz = ctx.MkInt(Convert.ToInt64(line.P1.z));
        
        var pvx = ctx.MkInt(Convert.ToInt64(line.Direction.x));
        var pvy = ctx.MkInt(Convert.ToInt64(line.Direction.y));
        var pvz = ctx.MkInt(Convert.ToInt64(line.Direction.z));
        
        var xLeft = ctx.MkAdd(x, ctx.MkMul(t, vx)); // x + t * vx
        var yLeft = ctx.MkAdd(y, ctx.MkMul(t, vy)); // y + t * vy
        var zLeft = ctx.MkAdd(z, ctx.MkMul(t, vz)); // z + t * vz
 
        var xRight = ctx.MkAdd(px, ctx.MkMul(t, pvx)); // px + t * pvx
        var yRight = ctx.MkAdd(py, ctx.MkMul(t, pvy)); // py + t * pvy
        var zRight = ctx.MkAdd(pz, ctx.MkMul(t, pvz)); // pz + t * pvz
 
        solver.Add(t >= 0); // time should always be positive - we don't want solutions for negative time
        solver.Add(ctx.MkEq(xLeft, xRight)); // x + t * vx = px + t * pvx
        solver.Add(ctx.MkEq(yLeft, yRight)); // y + t * vy = py + t * pvy
        solver.Add(ctx.MkEq(zLeft, zRight)); // z + t * vz = pz + t * pvz
    }
 
    solver.Check();
    var model = solver.Model;
 
    var rx = model.Eval(x);
    var ry = model.Eval(y);
    var rz = model.Eval(z);
 
    return Convert.ToInt64(rx.ToString()) + Convert.ToInt64(ry.ToString()) + Convert.ToInt64(rz.ToString());
}