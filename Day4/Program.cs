using Day4;

var lines = File.ReadAllLines("input.txt");

List<Card> cards = new();

foreach(var line in lines)
{
    var split1 = line.Split(":");
    int cardId = int.Parse(split1[0].Replace("Card ", ""));
    
    var split2 = split1[1].Split("|");
    
    var winningNumbers = split2[0].Split(" ").Where(x => !string.IsNullOrEmpty(x)).Select(x => int.Parse(x)).ToList();
    var numbers = split2[1].Split(" ").Where(x => !string.IsNullOrEmpty(x)).Select(x => int.Parse(x)).ToList();
    
    int matches = winningNumbers.Count(winningNumber => numbers.Contains(winningNumber));

    cards.Add(new Card()
    {
        CardId = cardId,
        WinningNumbers = winningNumbers,
        Numbers = numbers,
        NumberOfMatches = matches
    });
}

int sum = 0;

foreach (Card card in cards)
{
    if (card.NumberOfMatches == 1)
    {
        sum += 1;
        continue;
    }

    sum += (int)Math.Pow(2, card.NumberOfMatches - 1);
}

Console.WriteLine("Part 1: " + sum);

Dictionary<int, int> cardCopies = new();

foreach (Card card in cards)
{
    cardCopies.Add(card.CardId, 1);
}

foreach (Card card in cards)
{
    var copies = cardCopies[card.CardId];
    var matches = card.NumberOfMatches;
    
    for (int j = card.CardId + 1; j < card.CardId + matches + 1; j++)
    {
        cardCopies[j] += copies;
    }
}

sum = 0;
foreach (var numCopies in cardCopies.Values)
{
    sum += numCopies;
}

Console.WriteLine("Part 2: " + sum);