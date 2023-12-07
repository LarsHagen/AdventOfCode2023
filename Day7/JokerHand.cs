using System;
using static Day7.Hand;

namespace Day7
{
    internal class JokerHand : IComparable
    {
        public List<int> cards;
        public int bid;
        public HandType handType;

        public JokerHand(string input)
        {
            var split = input.Split(" ");
            bid = int.Parse(split[1]);

            cards = new();

            for (int i = 0; i < 5; i++)
            {
                char c = split[0][i];
                cards.Add(CardCToInt(c));
            }

            if (cards.Contains(1))
            {
                HandType bestHandType = HandType.HighCard;
                for (int alternativeCard = 2; alternativeCard <= 14; alternativeCard++)
                {
                    var alternativeHand = cards.Select(card => card == 1 ? alternativeCard : card).ToList();
                    var alternativeHandType = GetHandType(alternativeHand);

                    if (alternativeHandType > bestHandType)
                        bestHandType = alternativeHandType;
                }

                handType = bestHandType;
            }
            else
            {
                handType = GetHandType(cards);
            }
        }

        private int CardCToInt(char c) => c switch
        {
            'A' => 14,
            'K' => 13,
            'Q' => 12,
            'J' => 1,
            'T' => 10,
            '9' => 9,
            '8' => 8,
            '7' => 7,
            '6' => 6,
            '5' => 5,
            '4' => 4,
            '3' => 3,
            '2' => 2
        };

        private HandType GetHandType(List<int> cards)
        {
            var uniqueCards = cards.Distinct().ToList();
            var numUniqueCards = cards.Distinct().Count();

            //Five of kind
            if (numUniqueCards == 1)
                return HandType.FiveOfKind;

            //Full house or Four of kind
            if (numUniqueCards == 2)
            {
                var numFirst = cards.Count(c => c == cards[0]);

                if (numFirst == 2 || numFirst == 3)
                    return HandType.FullHouse;

                return HandType.FourOfKind;
            }

            //Three of kind or Two pair
            if (numUniqueCards == 3)
            {
                var countTypeA = cards.Count(c => c == uniqueCards[0]);
                var countTypeB = cards.Count(c => c == uniqueCards[1]);
                var countTypeC = cards.Count(c => c == uniqueCards[2]);

                if (countTypeA == 3 || countTypeB == 3 || countTypeC == 3)
                    return HandType.ThreeOfKind;

                return HandType.TwoPair;
            }

            //One pair
            if (numUniqueCards == 4)
                return HandType.OnePair;
    
            //High
            return HandType.HighCard;
        }

        public int CompareTo(object? obj)
        {
            var otherHand = obj as JokerHand;

            for (int i = 0; i < 5; i ++)
            {
                if (cards[i] < otherHand.cards[i])
                    return -1;
                if (cards[i] > otherHand.cards[i])
                    return 1;
            }

            return 0;
        }
    }
}
