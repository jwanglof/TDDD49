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
        private bool cardSelected = false;
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
        }   

        //Only for testcases
        public Card(int number, int suit, bool test)
        {
            InitializeComponent();

            this.number = number;
            this.suit = suit;
        }
        public int getNumber()
        {
            return number;
        }
        public int getSuit()
        {
            return suit;
        }
        public Boolean isSelected()
        {
            return cardSelected;
        }
 
        private void onMouseUp(object sender, MouseButtonEventArgs e)
        {
            cardSelected = !cardSelected;
            if (cardSelected)
                Canvas.SetBottom((Image)sender, 10);
            else
                Canvas.SetBottom((Image)sender, 0);
        }

        public void Reset()
        {
            cardSelected = false;
            Canvas.SetBottom(image, 0);
        }
    }
}
