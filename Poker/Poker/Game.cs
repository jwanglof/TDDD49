using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Poker
{
    class Game
    {
        private Deck deck = new Deck();
        private Money pot;
        private int startMoney = 200;
        private int turns = 0;
        private int totalTurns = 1;

        private int player1TotalBet = 0;
        private int player2TotalBet = 0;
        public Game(Money pot)
        {
            this.pot = pot;

            //GlobalVariables.player1.MouseUp += playerMouseUp; //Bra att ha kanske för kommunikation med spelar click men ändå tillgång till Game klassen
            GlobalVariables.player1.btnDone.Click += btnDoneClick;
            GlobalVariables.player2.btnDone.Click += btnDoneClick;
        }

        public void start()
        {
            giveStartMoney();
            giveCards();
            GlobalVariables.player1.toggleTurn(); //Player 1 begins
        }

        private void giveStartMoney()
        {
            GlobalVariables.player1.money.add(startMoney);
            GlobalVariables.player2.money.add(startMoney);
        }

        private void giveCards()
        {
            int cardsToGive = 5;
            for (int i = 0; i < cardsToGive; i++)
            {
                GlobalVariables.player1.addCard(deck.takeCard());
                GlobalVariables.player2.addCard(deck.takeCard());
            }
            GlobalVariables.player1.updateHand();
            GlobalVariables.player2.updateHand();     
        }

        //Not used, saved for possible later usage
        private void playerMouseUp(object sender, MouseButtonEventArgs e)
        {
            GlobalVariables.player1.clearCards();
            GlobalVariables.player2.clearCards();
            deck.resetDeck();
            giveCards();
        }

        private void btnDoneClick(object sender, RoutedEventArgs e)
        {
            /*
            Player currentPlayer;
            if (GlobalVariables.player1.turn())
                currentPlayer = GlobalVariables.player1;
            else
                currentPlayer = GlobalVariables.player2;

            List<Card> cardsToThrow = currentPlayer.removeSelectedCards();
            
            deck.throwCards(cardsToThrow);
            */

            turns += 1;

            if (turns == 2) //Change the selected cards, no toggle since its only 2 players at the momement every player will start a round every second time.
            {
                if (this.totalTurns == 3)
                {
                    // Game finished, reset everything and crown a winner!
                    Console.WriteLine("RESET EVERYTHING!!!!");

                    // Reset the pot
                    // Reset the deck
                    // Give the players new cards
                    giveCards();
                    GlobalVariables.player1.toggleTurn();
                    // Add to the victorious players pot
                    // Subtract from the losing players pot
                    //GlobalVariables.player1.subtractFromMoney(pot.getMoney());
                    //GlobalVariables.player2.addToMoney(pot.getMoney());
                }
                else
                {
                    turns = 0;
                    this.totalTurns += 1;

                    List<Card> cardsToThrow = GlobalVariables.player1.removeSelectedCards();
                    GlobalVariables.player1.addCards(deck.takeCards(cardsToThrow.Count));
                    deck.throwCards(cardsToThrow);
                    
                    cardsToThrow = GlobalVariables.player2.removeSelectedCards();
                    GlobalVariables.player2.addCards(deck.takeCards(cardsToThrow.Count));
                    deck.throwCards(cardsToThrow);

                    pot.add(GlobalVariables.player1.getPrevBet() + GlobalVariables.player2.getPrevBet());

                    // Reset both player's betting
                    GlobalVariables.player1.setPrevBet(0);
                    GlobalVariables.player2.setPrevBet(0);
                    GlobalVariables.player1.betCounter.Text = "0";
                    GlobalVariables.player2.betCounter.Text = "0";

                    // To keep track of how much each player has played for in total
                    this.player1TotalBet += GlobalVariables.player1.getPrevBet();
                    this.player2TotalBet += GlobalVariables.player2.getPrevBet();
                }
            }
            else
            {
                GlobalVariables.player1.toggleTurn();
                GlobalVariables.player2.toggleTurn();
            }   
        }
    }
}
