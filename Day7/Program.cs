using Day7;

var lines = File.ReadAllLines("input.txt");

List<Hand> hands = new();
List<Hand.HandType> handTypes = new() {
    Hand.HandType.HighCard,
    Hand.HandType.OnePair,
    Hand.HandType.TwoPair,
    Hand.HandType.ThreeOfKind,
    Hand.HandType.FullHouse,
    Hand.HandType.FourOfKind,
    Hand.HandType.FiveOfKind };

foreach (var line in lines)
{
    hands.Add(new Hand(line));
}

Dictionary<Hand.HandType, List<Hand>> SortedByType = new();

foreach (var handType in handTypes)
    SortedByType.Add(handType, new());

foreach (var hand in hands)
{
    SortedByType[hand.handType].Add(hand);
}


int count = 1;
int sum = 0;

foreach (var handType in handTypes)
{
    SortedByType[handType].Sort();

    foreach (var hand in SortedByType[handType])
    {
        var score = hand.bid * count;
        sum += score;
        count++;
    }
}

Console.WriteLine("Part 1: " + sum);



List<JokerHand> part2Hands = new();

foreach (var line in lines)
{
    part2Hands.Add(new JokerHand(line));
}

Dictionary<Hand.HandType, List<JokerHand>> Part2SortedByType = new();

foreach (var handType in handTypes)
    Part2SortedByType.Add(handType, new());

foreach (var hand in part2Hands)
{
    Part2SortedByType[hand.handType].Add(hand);
}


count = 1;
sum = 0;

foreach (var handType in handTypes)
{
    Part2SortedByType[handType].Sort();

    foreach (var hand in Part2SortedByType[handType])
    {
        var score = hand.bid * count;
        sum += score;
        count++;
    }
}

Console.WriteLine("Part 2: " + sum);
