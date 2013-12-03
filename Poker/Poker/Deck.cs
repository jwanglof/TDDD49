using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public static class Deck
    {
        private static List<Card> allCards = new List<Card>();
        private static List<Card> cardsInDeck = new List<Card>();
        private static List<Card> thrownCards = new List<Card>();

        public static void initCards()
        {
            for (int suit = 1; suit <= 4; suit++)
                for (int number = 1; number <= 13; number++)
                    allCards.Add(new Card(number, suit));

            resetDeck();
        }
        public static void resetDeck()
        {
            cardsInDeck = allCards;
            shuffleCards();
        }

        private static void shuffleCards()
        {
            List<Card> shuffled = new List<Card>();
            Random rand = new Random();

            while(cardsInDeck.Count > 0)
            {
                int index = rand.Next(cardsInDeck.Count);
                shuffled.Add(cardsInDeck[index]);
                cardsInDeck.RemoveAt(index);
            }
            cardsInDeck = shuffled;
        }

        public static Card takeCard()
        {
            //If deck i empty, add thrown cards and shuffle
            if (cardsInDeck.Count == 0)
            {
                cardsInDeck = thrownCards;
                shuffleCards();
            }

            Card card = cardsInDeck[cardsInDeck.Count - 1];
            cardsInDeck.RemoveAt(cardsInDeck.Count - 1);

            return card;
        }

        public static void throwCard(Card card)
        {
            thrownCards.Add(card);
        }
    }
}
