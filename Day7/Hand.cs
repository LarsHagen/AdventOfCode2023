using System;

namespace Day7
{
    internal class Hand : IComparable
    {
        public enum HandType { HighCard = 0, OnePair = 1, TwoPair = 2, ThreeOfKind = 3, FullHouse = 4, FourOfKind = 5, FiveOfKind = 6};

        public List<int> cards;
        public int bid;
        public HandType handType;

        public Hand(string input)
        {
            var split = input.Split(" ");
            bid = int.Parse(split[1]);

            cards = new();

            for (int i = 0; i < 5; i++)
            {
                char c = split[0][i];
                cards.Add(CardCToInt(c));
            }

            handType = GetHandType();
        }

        private int CardCToInt(char c) => c switch
        {
            'A' => 14,
            'K' => 13,
            'Q' => 12,
            'J' => 11,
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

        private HandType GetHandType()
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
            var otherHand = obj as Hand;

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
