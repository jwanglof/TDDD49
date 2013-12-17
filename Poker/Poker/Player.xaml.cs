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
            List<Card> selected = hand.Where(card => card.cardSelected).ToList();
            foreach (Card card in selected)
            {
                card.cardSelected = false;
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
                turnGrid.Visibility = System.Windows.Visibility.Visible;
            else
                turnGrid.Visibility = System.Windows.Visibility.Hidden;
        }

        private void addBet(object sender, RoutedEventArgs e)
        {
            // Need to check so that the player can't bet above his current cash
            if (this.prevBet < money.getMoney())
            {
                this.prevBet += 1;
                betCounter.Text = this.prevBet.ToString();
            }
        }
        private void addBet10(object sender, RoutedEventArgs e)
        {
            // Need to check so that the player can't bet above his current cash
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
        }

        private void subtrBet(object sender, RoutedEventArgs e)
        {
            if (this.prevBet > 0)
            {
                this.prevBet -= 1;
                betCounter.Text = this.prevBet.ToString();
            }
            else
            {
                this.prevBet = 0;
                betCounter.Text = "0";
            }
        }

        private void subtrBet10(object sender, RoutedEventArgs e)
        {
            if (this.prevBet > 10)
            {
                this.prevBet -= 10;
                betCounter.Text = this.prevBet.ToString();
            }
            else
            {
                this.prevBet = 0;
                betCounter.Text = "0";
            }
        }

        private void resetBet(object sender, RoutedEventArgs e)
        {
            this.prevBet = 0;
            betCounter.Text = "0";
        }

        public int getPrevBet()
        {
            return this.prevBet;
        }
    }
}
