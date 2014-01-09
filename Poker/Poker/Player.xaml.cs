using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Poker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Player : UserControl
    {
        private List<Card> hand = new List<Card>();
        private bool myTurn = false;
        private int prevBet = 0;

        public Player()
        {
            InitializeComponent();
        }

        /*
        * Returns an entity of the object
        */
        public Players getEntity()
        {
            Players entity = new Players();

            entity.card1_suit = hand[0].getSuit();
            entity.card1_value = hand[0].getNumber();
            entity.card2_suit = hand[1].getSuit();
            entity.card2_value = hand[1].getNumber();
            entity.card3_suit = hand[2].getSuit();
            entity.card3_value = hand[2].getNumber();
            entity.card4_suit = hand[3].getSuit();
            entity.card4_value = hand[3].getNumber();
            entity.card5_suit = hand[4].getSuit();
            entity.card5_value = hand[4].getNumber();

            entity.money = money.getMoney();
            entity.myTurn = myTurn;
            entity.prevBet = prevBet;

            return entity;
        }

        /*
         * Modifies this object according the the entity properties
         */ 
        public void parsePlayerEntity(Players entity, List<Card> hand)
        {
            //Load turn
            myTurn = entity.myTurn;
            cardGrid.IsEnabled = myTurn;
            if (myTurn)
            {
                turnGrid.Visibility = System.Windows.Visibility.Visible;
                betGrid.IsEnabled = true;
            }
            else
            {
                turnGrid.Visibility = System.Windows.Visibility.Hidden;
                betGrid.IsEnabled = false;
            }

            //Load bet
            prevBet = entity.prevBet;
            betCounter.Text = prevBet.ToString();
            money.setMoney(entity.money);

            //Load cards
            this.hand = hand;
            updateHand();
        }

        public void addCard(Card card)
        {
            hand.Add(card);
        }

        public void addCards(List<Card> cards)
        {
            hand.AddRange(cards);
            updateHand();
        }
        
        /*
         * Updates the GUI with the current hand
         */ 
        public void updateHand()
        {
            cardGrid.Children.Clear();
            for (int i = 0; i < hand.Count(); i++)
            {
                cardGrid.Children.Add(hand[i]);
                Grid.SetColumn(hand[i], i);
            }
        }

        public List<Card> getCards()
        {
            return hand;
        }

        public List<Card> removeSelectedCards()
        {
            List<Card> selectedCards = new List<Card>();
            List<Card> selected = hand.Where(card => card.isSelected()).ToList();
            foreach (Card card in selected)
            {
                selectedCards.Add(card);
                hand.Remove(card);
            }
            return selectedCards;
        }

        public void clearCards()
        {
            hand.ForEach(card => card.Reset());
            cardGrid.Children.Clear();
            hand.Clear();
        }

        public void toggleTurn()
        {
            myTurn = !myTurn;
            cardGrid.IsEnabled = myTurn;
            if (myTurn)
            {
                turnGrid.Visibility = System.Windows.Visibility.Visible;
                betGrid.IsEnabled = true;
            }
            else
            {
                turnGrid.Visibility = System.Windows.Visibility.Hidden;
                betGrid.IsEnabled = false;
            }
        }

        // Add 1 to the player's bet
        private void addBet(object sender, RoutedEventArgs e)
        {
            // Need to check so that the player can't bet mote then his current cash
            if (this.prevBet < money.getMoney())
            {
                this.prevBet += 1;
                betCounter.Text = this.prevBet.ToString();
            }

            btnDone.Content = "Raise";
        }

        // Add 10 to the player's bet
        private void addBet10(object sender, RoutedEventArgs e)
        {
            // Need to check so that the player can't bet more then his current cash
            if (this.prevBet < money.getMoney()-10)
            {
                this.prevBet += 10;
                betCounter.Text = this.prevBet.ToString();
            }
            else
            {
                this.prevBet = money.getMoney();
                betCounter.Text = this.prevBet.ToString();
            }

            btnDone.Content = "Raise";
        }

        // Subtract 1 from the player's bet
        private void subtrBet(object sender, RoutedEventArgs e)
        {
            if (this.prevBet > 0)
            {
                this.prevBet -= 1;
                betCounter.Text = this.prevBet.ToString();
                btnDone.Content = "Raise";
            }
            // If the player tries to lower his bet below 0 it won't go below it
            else
            {
                this.prevBet = 0;
                betCounter.Text = "0";
                btnDone.Content = "Call";
            }
        }

        // Subtract 10 from the player's bet
        private void subtrBet10(object sender, RoutedEventArgs e)
        {
            // If the player has more than 10 money it will subtract 10 from the bet
            if (this.prevBet > 10)
            {
                this.prevBet -= 10;
                betCounter.Text = this.prevBet.ToString();
                btnDone.Content = "Raise";
            }
            // If the player tries to lower his bet below 0 it won't go below it
            // or if the player has less than 10 money it will go down to 0
            else
            {
                this.prevBet = 0;
                betCounter.Text = "0";
                btnDone.Content = "Call";
            }
        }

        // Reset the bet counter
        private void resetBet(object sender, RoutedEventArgs e)
        {
            this.prevBet = 0;
            betCounter.Text = "0";
            btnDone.Content = "Call";
        }

        private void allIn(object sender, RoutedEventArgs e)
        {
            this.prevBet = money.getMoney();
            betCounter.Text = this.prevBet.ToString();
            btnDone.Content = "All in";
        }

        // Get previous bet so it can be added to the pot and subtracted from the player's money
        public int getPrevBet()
        {
            return this.prevBet;
        }
        public void setPrevBet(int newValue)
        {
            this.prevBet = newValue;
        }

        public void subtractFromMoney(int value)
        {
            money.remove(value);
        }

        public void addToMoney(int value)
        {
            money.add(value);
        }
    }
}
