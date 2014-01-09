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
    public class Game
    {
        private Deck deck = new Deck();
        private Rules rules = new Rules();

        private Money pot;
        private int startMoney = 200;
        private int player1TotalBet = 0;
        private int player2TotalBet = 0;

        private int turns = 0; //Turns of all players, resets at 2 as we only got 2 players
        private int totalTurns = 1; //Turns of each player

        public Game(Money pot)
        {
            this.pot = pot;

            GlobalVariables.player1.btnDone.Click += btnDoneClick;
            GlobalVariables.player2.btnDone.Click += btnDoneClick;
        }

        /*
         * Returns an entity of the object
         */
        public Games getEntity()
        {
            Games entity = new Games();

            //Save turn
            entity.totalTurns = totalTurns;
            entity.turns = turns;

            //Save pool
            entity.pool = pot.getMoney();

            //Save players
            Players player1Entity = GlobalVariables.player1.getEntity();
            player1Entity.totalBet = player1TotalBet;
            entity.Players.Add(player1Entity);

            Players player2Entity = GlobalVariables.player2.getEntity();
            player2Entity.totalBet = player2TotalBet;
            entity.Players.Add(player2Entity);

            //Save thrown cards
            foreach(ThrownCards cardEntity in deck.getTrownCardsEntities())
                entity.ThrownCards.Add(cardEntity);

            return entity;
        }

        /*
         * Modifies this object according the the entity properties
         */ 
        public void parseGameEntity(Games entity)
        {
            //Load turn
            turns = entity.turns;
            totalTurns = entity.totalTurns;

            //Load pool
            pot.setMoney(entity.pool);

            //Load players
            List<Players> playerEntities = entity.Players.ToList();
            GlobalVariables.player1.parsePlayerEntity(playerEntities[0], deck.getPlayerEntityCards(playerEntities[0]));
            GlobalVariables.player2.parsePlayerEntity(playerEntities[1], deck.getPlayerEntityCards(playerEntities[1]));

            player1TotalBet = playerEntities[0].totalBet;
            player2TotalBet = playerEntities[1].totalBet;

            //Load thrown cards
            List<ThrownCards> thrownCardsEntities = entity.ThrownCards.ToList();
            deck.parseThrownCardEntities(thrownCardsEntities);
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

        /*
         * Deal cards to the players
         */
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

        private void btnDoneClick(object sender, RoutedEventArgs e)
        {
            turns += 1;

            //Change the selected cards, no toggle since its only 2 players at the momement every player will start a round every second time.
            if (turns >= 2) 
            {
                //After 3 turns the round is done
                if (this.totalTurns >= 3)
                {
                    // Check for winner
                    int winner = rules.checkBestHand(GlobalVariables.player1.getCards(), GlobalVariables.player2.getCards());

                    // Add to the victorious players pot
                    // Subtract from the losing players pot
                    if (winner == 1)
                    {
                        Console.WriteLine("Player 1 won!");
                        GlobalVariables.player1.addToMoney(pot.getMoney());
                        GlobalVariables.player2.subtractFromMoney(this.player2TotalBet);
                    }
                    else if (winner == 2)
                    {
                        Console.WriteLine("Player 2 won!");
                        GlobalVariables.player2.addToMoney(pot.getMoney());
                        GlobalVariables.player1.subtractFromMoney(this.player1TotalBet);
                    }

                    //Reset for new round
                    this.totalTurns = 1;
                    this.player1TotalBet = 0;
                    this.player2TotalBet = 0;
                    this.pot = new Money();

                    GlobalVariables.player1.clearCards();
                    GlobalVariables.player2.clearCards();
                    deck.resetDeck();
                    giveCards();

                    GlobalVariables.player1.toggleTurn();
                    GlobalVariables.player2.toggleTurn();
                }
                else
                {
                    turns = 0;
                    this.totalTurns += 1;

                    //Throw and get new cards
                    List<Card> cardsToThrow = GlobalVariables.player1.removeSelectedCards();
                    GlobalVariables.player1.addCards(deck.takeCards(cardsToThrow.Count));
                    deck.throwCards(cardsToThrow);

                    cardsToThrow = GlobalVariables.player2.removeSelectedCards();
                    GlobalVariables.player2.addCards(deck.takeCards(cardsToThrow.Count));
                    deck.throwCards(cardsToThrow);

                    //Add bets to the pot
                    pot.add(GlobalVariables.player1.getPrevBet() + GlobalVariables.player2.getPrevBet());

                    GlobalVariables.player1.subtractFromMoney(GlobalVariables.player1.getPrevBet());
                    GlobalVariables.player2.subtractFromMoney(GlobalVariables.player2.getPrevBet());

                    // To keep track of how much each player has played for in total
                    this.player1TotalBet += GlobalVariables.player1.getPrevBet();
                    this.player2TotalBet += GlobalVariables.player2.getPrevBet();
                }

                // Reset both player's betting
                GlobalVariables.player1.setPrevBet(0);
                GlobalVariables.player2.setPrevBet(0);
                GlobalVariables.player1.betCounter.Text = "0";
                GlobalVariables.player2.betCounter.Text = "0";
            }
            else
            {
                //Toggle turn
                GlobalVariables.player1.toggleTurn();
                GlobalVariables.player2.toggleTurn();
            }   
        }
    }
}
