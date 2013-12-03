using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Game
    {

        public Game()
        {
            Deck.initCards();
        }

        public void start()
        {
            giveCoins();
            giveCards();
        }

        private void giveCoins()
        {
            //not implemented yet
        }

        private void giveCards()
        {
            int cardsToGive = 5;
            for (int i = 0; i < cardsToGive; i++)
            {
                GlobalVariables.player1.addCard(Deck.takeCard());
                GlobalVariables.player2.addCard(Deck.takeCard());
            }
            GlobalVariables.player1.updateHand();
            GlobalVariables.player2.updateHand();
        }
    }
}
