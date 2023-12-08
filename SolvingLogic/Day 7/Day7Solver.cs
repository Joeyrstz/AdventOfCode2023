using System.Reflection;

namespace SolvingLogic.Day_7;

public static class Day7Solver
{
    private static Dictionary<char, int> cardValue = new()
    {
        {'2', 2},
        {'3', 3},
        {'4', 4},
        {'5', 5},
        {'6', 6},
        {'7', 7},
        {'8', 8},
        {'9', 9},
        {'T', 10},
        {'J', 11},
        {'Q', 12},
        {'K', 13},
        {'A', 14},
    };
    private static Dictionary<char, int> cardValue2 = new()
    {
        {'J', 1},
        {'2', 2},
        {'3', 3},
        {'4', 4},
        {'5', 5},
        {'6', 6},
        {'7', 7},
        {'8', 8},
        {'9', 9},
        {'T', 10},
        {'Q', 11},
        {'K', 12},
        {'A', 13},
    };
    private enum CardType
    {
        HighCard = 1,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind,
    }
    public static string[] GetInput()
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        path = Path.Combine(path, "Day 7", "Input.txt");
        return File.ReadAllLines(path);
    }

    public static int SolveTask1(string[] input)
    {
        var result = 0;
        var camelCards = new List<Camelcard>();
        foreach (var currentLine in input)
        {
            var splittedLine = currentLine.Split(" ");
            camelCards.Add(new Camelcard
            {
                Cards = splittedLine[0].ToCharArray(),
                Bid = int.Parse(splittedLine[1])
            });
        }
        
        Dictionary<CardType, List<Camelcard>> cardTypeDictionary = new() 
        {
            {CardType.HighCard, [] },
            {CardType.OnePair, [] },
            {CardType.TwoPair, [] },
            {CardType.ThreeOfAKind, [] },
            {CardType.FullHouse, [] },
            {CardType.FourOfAKind, [] },
            {CardType.FiveOfAKind, [] },
        };

        foreach (var currentCard in camelCards)
        {
            var cardType = GetType(currentCard);
            cardTypeDictionary[cardType].Add(currentCard);
        }
        
        Comparison<char[]> valueComparison = (x, y) =>
        {
            for (var i = 0; i < x.Length; i++)
            {
                if(cardValue[x[i]] == cardValue[y[i]]) continue;
                return cardValue[x[i]] > cardValue[y[i]] ? 1 : -1;
            }

            return 0;
        };
        foreach (var list in cardTypeDictionary.Select(pair => pair.Value))
        {
            list.Sort((x, y) => valueComparison(x.Cards, y.Cards));
        }

        var pointCounter = 1;
        for (int i = 1; i <= 7; i++)
        {
            var currentList = cardTypeDictionary[(CardType)i];
            foreach (var currentCard in currentList)
            {
                result += currentCard.Bid * pointCounter++;
            }
        }
        
        
        return result;
    }
    
    public static int SolveTask2(string[] input)
    {
        var result = 0;
        var camelCards = new List<Camelcard>();
        foreach (var currentLine in input)
        {
            var splittedLine = currentLine.Split(" ");
            camelCards.Add(new Camelcard
            {
                Cards = splittedLine[0].ToCharArray(),
                Bid = int.Parse(splittedLine[1])
            });
        }
        
        Dictionary<CardType, List<Camelcard>> cardTypeDictionary = new() 
        {
            {CardType.HighCard, [] },
            {CardType.OnePair, [] },
            {CardType.TwoPair, [] },
            {CardType.ThreeOfAKind, [] },
            {CardType.FullHouse, [] },
            {CardType.FourOfAKind, [] },
            {CardType.FiveOfAKind, [] },
        };

        foreach (var currentCard in camelCards)
        {
            var cardType = GetTypeWithJoker(currentCard);
            cardTypeDictionary[cardType].Add(currentCard);
        }
        
        Comparison<char[]> valueComparison = (x, y) =>
        {
            for (var i = 0; i < x.Length; i++)
            {
                if(cardValue2[x[i]] == cardValue2[y[i]]) continue;
                return cardValue2[x[i]] > cardValue2[y[i]] ? 1 : -1;
            }

            return 0;
        };
        foreach (var list in cardTypeDictionary.Select(pair => pair.Value))
        {
            list.Sort((x, y) => valueComparison(x.Cards, y.Cards));
        }

        var pointCounter = 1;
        for (int i = 1; i <= 7; i++)
        {
            var currentList = cardTypeDictionary[(CardType)i];
            foreach (var currentCard in currentList)
            {
                result += currentCard.Bid * pointCounter++;
            }
        }
        
        
        return result;
    }


    private static CardType GetTypeWithJoker(Camelcard card)
    {
        var jokerCount = card.Cards.Count(x => x == 'J');
        if (jokerCount == 0)
        {
            return GetType(card);
        }

        var bestCardType = CardType.HighCard;
        var allJokerCombination = new List<char[]>();
        var characters = card.Cards.Distinct();
        foreach (var uniqueChar in characters)
        {
            var tempText = new string(card.Cards).Replace('J', uniqueChar);
            allJokerCombination.Add(tempText.ToCharArray());
        }

        foreach (var jokerCombination in allJokerCombination)
        {
            var tempCard = new Camelcard
            {
                Cards = jokerCombination,
                Bid = card.Bid
            };
            var tempCardType = GetType(tempCard);
            if (tempCardType > bestCardType)
            {
                bestCardType = tempCardType;
            }
        }

        return bestCardType;
    }
    
    private static CardType GetType(Camelcard card)
    {
        
        //check for five of a kind
        if (card.Cards.All(x => x == card.Cards[0]))
        {
            return CardType.FiveOfAKind;
        }
        
        //check for four of a kind
        if (card.Cards.GroupBy(x => x).Any(x => x.Count() == 4))
        {
            return CardType.FourOfAKind;
        }
        
        //check for full house
        if (card.Cards.GroupBy(x => x).Any(x => x.Count() == 3) && card.Cards.GroupBy(x => x).Any(x => x.Count() == 2))
        {
            return CardType.FullHouse;
        }
        
        //check for three of a kind
        if (card.Cards.GroupBy(x => x).Any(x => x.Count() == 3))
        {
            return CardType.ThreeOfAKind;
        }
        
        //check for two pair
        if (card.Cards.GroupBy(x => x).Count(x => x.Count() == 2) == 2)
        {
            return CardType.TwoPair;
        }
        
        //check for one pair
        if (card.Cards.GroupBy(x => x).Any(x => x.Count() == 2))
        {
            return CardType.OnePair;
        }
        
        //it must be a high card
        return CardType.HighCard;
    }
    
    
}