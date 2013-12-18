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
    public partial class Money : UserControl
    {
        private int money = 0;
        public Money()
        {
            InitializeComponent();
        }
        
        public void add(int amount)
        {
            Console.WriteLine("Added money, amount=" + amount);
            money += amount;
            updateGUI();
        }

        public bool remove(int amount)
        {
            Console.WriteLine("Removed money, amount=" + amount);
            if (amount > money)
                return false;

            money -= amount;
            updateGUI();
            return true;
        }

        public int removeAll()
        {
            int bet = money;
            money = 0;
            updateGUI();
            return bet;
        }

        private void updateGUI()
        {
            lblValue.Content = money;     
        }

        public int getMoney()
        {
            return this.money;
        }
    }
}
