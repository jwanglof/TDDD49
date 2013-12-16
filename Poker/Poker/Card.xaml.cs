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
    public partial class Card : UserControl
    {
        public int number = 0;
        private int suit = 1;
        public Card()
        {
            InitializeComponent();
        }
        public Card(int number, int suit)
        {
            InitializeComponent();

            this.number = number;
            this.suit = suit;

            string imagePath = "DeckOfCards/"+GlobalVariables.toCardSuit(suit)+GlobalVariables.toCardValue(number)+".png";
            image.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
          
            //lblNumber.Content = GlobalVariables.toCardValue(number);
            //lblSuit.Content = GlobalVariables.toCardSuit(suit);
        }        
        public int getNumber()
        {
            return number;
        }
        public int getSuit()
        {
            return number;
        }

        private void onMouseUp(object sender, MouseButtonEventArgs e)
        {
            //lblNumber.Content = "99";
            //lblSuit.Content = "66";
        }

        
    }
}
