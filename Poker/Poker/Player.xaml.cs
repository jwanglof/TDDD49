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
        public Player()
        {
            InitializeComponent();
        }

        public void addCard(Card card)
        {
            hand.Add(card);
            Console.WriteLine("added card!");       
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

        public void throwCard(Card card)
        {
            hand.Remove(card);
            Console.WriteLine("removed card!");
        }
    }
}
