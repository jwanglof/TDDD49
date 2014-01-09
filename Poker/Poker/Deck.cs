using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class Deck
    {
        private List<Card> allCards = new List<Card>();
        private List<Card> cardsInDeck = new List<Card>();
        private List<Card> thrownCards = new List<Card>();

        public Deck()
        {
            for (int suit = 1; suit <= 4; suit++)
                for (int number = 2; number <= 14; number++)
                    allCards.Add(new Card(number, suit));

            resetDeck();
        }

        public void parseThrownCardEntities(List<ThrownCards> thrownCardsEntities)
        {
            List<Card> cardsToThrow = new List<Card>();
            foreach(ThrownCards entity in thrownCardsEntities)
                cardsToThrow.Add(findCard(entity.suit, entity.value));

            resetDeck();
            throwCards(cardsToThrow);
        }

        public List<Card> getPlayerEntityCards(Players playerEntity)
        {
            List<Card> cards = new List<Card>();
            cards.Add(findCard(playerEntity.card1_suit, playerEntity.card1_value));
            cards.Add(findCard(playerEntity.card2_suit, playerEntity.card2_value));
            cards.Add(findCard(playerEntity.card3_suit, playerEntity.card3_value));
            cards.Add(findCard(playerEntity.card4_suit, playerEntity.card4_value));
            cards.Add(findCard(playerEntity.card5_suit, playerEntity.card5_value));

            foreach (Card card in cards)
                cardsInDeck.Remove(card);

            return cards;
        }

        public void resetDeck()
        {
            cardsInDeck.Clear();
            for (int i = 0; i < allCards.Count; i++)
                cardsInDeck.Add(allCards[i]);
            shuffleCards();
        }
        private void shuffleCards()
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
        public Card takeCard()
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

        public List<Card> takeCards(int amount)
        {
            List<Card> cards = new List<Card>();
            for (int i = 0; i < amount; i++)
                cards.Add(takeCard());
            return cards;
        }

        public void throwCards(List<Card> cards)
        {
            cards.ForEach(card => card.Reset());
            thrownCards.AddRange(cards);
        }

        private Card findCard(int suit, int value)
        {
            foreach (Card card in allCards)
                if (card.getSuit() == suit && card.getNumber() == value)
                    return card;
            return null;
        }

        public List<ThrownCards> getTrownCardsEntities()
        {
            List<ThrownCards> entities = new List<ThrownCards>();
            
            foreach(Card card in thrownCards)
            {
                ThrownCards entity = new ThrownCards();
                entity.suit = card.getSuit();
                entity.value = card.getNumber();

                entities.Add(entity);
            }

            return entities;
        }

    }
}
