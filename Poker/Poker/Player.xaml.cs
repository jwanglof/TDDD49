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

        public void parsePlayerEntity(Players entity, List<Card> hand)
        {
            myTurn = entity.myTurn;
            prevBet = entity.prevBet;
            money.setMoney(entity.money);
            this.hand = hand;
            updateHand();

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

        public void addCard(Card card)
        {
            hand.Add(card);
            Console.WriteLine("added card!");       
        }

        public void addCards(List<Card> cards)
        {
            hand.AddRange(cards);
            updateHand();
            Console.WriteLine("added " + cards.Count + " cards!");
        }
        
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

        public void throwCard(Card card)
        {
            hand.Remove(card);
            Console.WriteLine("removed card!");
        }

        public void clearCards()
        {
            hand.ForEach(card => card.Reset());
            cardGrid.Children.Clear();
            hand.Clear();
        }

        public bool turn()
        {
            return myTurn;
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

        private void subtrBet(object sender, RoutedEventArgs e)
        {
            if (this.prevBet > 0)
            {
                this.prevBet -= 1;
                betCounter.Text = this.prevBet.ToString();
                btnDone.Content = "Raise";
            }
            else
            {
                this.prevBet = 0;
                betCounter.Text = "0";
                btnDone.Content = "Call";
            }
        }

        private void subtrBet10(object sender, RoutedEventArgs e)
        {
            if (this.prevBet > 10)
            {
                this.prevBet -= 10;
                betCounter.Text = this.prevBet.ToString();
                btnDone.Content = "Raise";
            }
            else
            {
                this.prevBet = 0;
                betCounter.Text = "0";
                btnDone.Content = "Call";
            }
        }

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
