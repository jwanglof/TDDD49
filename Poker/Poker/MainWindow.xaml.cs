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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            GlobalVariables.initGlobalVariables();
            GlobalVariables.player1 = player1;
            GlobalVariables.player2 = player2;

            Game game = new Game(pot);
            game.start();
        }

        public void saveState(object sender, RoutedEventArgs e)
        {
            gameNamePopup.Visibility = System.Windows.Visibility.Visible;
            
        }

        public void stateCloseWindow(object sender, RoutedEventArgs e)
        {
            gameNamePopup.Visibility = System.Windows.Visibility.Hidden;
        }

        public void stateSaveGame(object sender, RoutedEventArgs e)
        {
            gameNamePopup.Visibility = System.Windows.Visibility.Hidden;

            using (var db = new StateContext())
            {
                String gameName = stateName.Text;

                var state = new State { stateTitle = gameName };
                db.States.Add(state);
                db.SaveChanges();

                Console.WriteLine("State is saved as " + gameName + "!");
            }
        }

        public void loadState(object sender, RoutedEventArgs e)
        {
            using (var db = new StateContext())
            {
                var query = from b in db.States
                            orderby b.stateTitle
                            select b;

                foreach (var item in query)
                {
                    Console.WriteLine(item.id);
                    Console.WriteLine(item.stateTitle);
                }

                var state = db.States.Find(1);
            }
        }
    }
}
