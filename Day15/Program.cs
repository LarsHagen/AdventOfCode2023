
var input = File.ReadAllText("input.txt");
var steps = input.Split(",");

int sum = steps.Sum(step => Hash(step));
Console.WriteLine("Part 1: " + sum);

Dictionary<int, List<LabeledLens>> boxes = new();
for (int i = 0; i < 256; i++)
    boxes.Add(i, new());

foreach (var step in steps)
{
    var splitSign = step.Contains('=') ? '=' : '-';
    var label = step.Split(splitSign)[0];
    var boxIndex = Hash(label);
    var box = boxes[boxIndex];
    
    if (step.Contains('='))
    {
        var lens = int.Parse(step.Split('=')[1]);

        if (box.Any(b => b.Label == label))
            box.First(b => b.Label == label).Lens = lens;
        else
            box.Add(new LabeledLens {Label = label, Lens = lens});
    }
    else
    {
        var labeledLens = box.FirstOrDefault(l => l.Label == label);
        if (labeledLens != null)
            box.Remove(labeledLens);
    }
}


sum = 0;
foreach (var box in boxes)
{
    int boxIndex = box.Key;
    for (int lensIndex = 0; lensIndex < box.Value.Count; lensIndex++)
    {
        var labeledLens = box.Value[lensIndex];
        var focusPower = FocusPower(boxIndex, lensIndex, labeledLens.Lens);
        sum += focusPower;
    }
}

Console.WriteLine("Part 2: " + sum);

int Hash(string input)
{
    int hash = 0;
    foreach (var c in input)
    {
        int ascii = c;
        hash += ascii;
        hash *= 17;
        hash %= 256;
    }

    return hash;
}

int FocusPower(int boxIndex, int slotIndex, int focalLength)
{
    return (boxIndex + 1) * (slotIndex + 1) * focalLength;
}

class LabeledLens
{
    public string Label;
    public int Lens;
}