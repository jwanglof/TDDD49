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
        private Game game;
        public MainWindow()
        {
            InitializeComponent();

            GlobalVariables.initGlobalVariables();
            GlobalVariables.player1 = player1;
            GlobalVariables.player2 = player2;

            game = new Game(pot);
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


            /*using (var db = new StatesContext())
            {
                //try
                //{
                    String gameName = stateName.Text;

                    var state = new States() { 
                        stateTitle = gameName, 
                        player1Cards = GlobalVariables.player1.getCards(),
                        player2Cards = GlobalVariables.player2.getCards(),
                        player1Money = GlobalVariables.player1.money.getMoney(),
                        player2Money = GlobalVariables.player2.money.getMoney()
                    };

                    /*
                    state.player1Cards = GlobalVariables.player1.getCards();
                    state.player2Cards = GlobalVariables.player2.getCards();

                    state.player1Money = GlobalVariables.player1.money.getMoney();
                    state.player2Money = GlobalVariables.player2.money.getMoney();
                    
                    
                    // Does not work....
                    db.States.Add(state);
                    
                    db.SaveChanges();

                    Console.WriteLine("State is saved as: " + gameName + "!");
                }
                catch (NotSupportedException)
                {
                    Console.WriteLine("Something went wrong!");
                }*/
            }*/
        }

        public void loadState(object sender, RoutedEventArgs e)
        {
            /*using (var db = new StatesContext())
            {
                var query = 

                Console.WriteLine("Saved states:");
                foreach (var item in query)
                {
                    //Console.WriteLine("ID: "+ item.id +". Title: "+ item.stateTitle);
                    Console.WriteLine(item);
                }

                var state = db.Game.Find(1);
            }*/
        }
    }
}
