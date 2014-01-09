﻿using System;
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

        public void parseEntities(List<ThrownCards> thrownCardsEntities, Players player1Entity, Players player2Entity)
        {

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
