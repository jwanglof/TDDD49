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

        public void onClickSave(object sender, RoutedEventArgs e)
        {
            // Toggle visibility on the board's button
            if (gameNamePopup.Visibility == System.Windows.Visibility.Visible)
                gameNamePopup.Visibility = System.Windows.Visibility.Hidden;
            else
                gameNamePopup.Visibility = System.Windows.Visibility.Visible;

            closeSaveWindow.Click += closeSaveWindow_Click;

            saveGame.Click += (asdd, args) =>
            {
                Console.WriteLine("before");
                Games gameEntity = game.getEntity();

                gameEntity.name = stateName.Text;

                DatabaseEntities db = new DatabaseEntities();
                db.Games.Add(gameEntity);
                db.SaveChanges();

                Console.WriteLine("Save done!");

                IQueryable<Games> custQuery =
                    from entry in db.Games
                    select entry;
                List<Games> x = custQuery.ToList();
                foreach (Games t in x)
                    Console.WriteLine(t.Id + "  " + t.name );

                gameNamePopup.Visibility = System.Windows.Visibility.Hidden;
                stateName.Text = "";
            };
        }

        public void onClickLoad(object sender, RoutedEventArgs e)
        {
            if (loadGamePopup.Visibility == System.Windows.Visibility.Visible)
                loadGamePopup.Visibility = System.Windows.Visibility.Hidden;
            else
                loadGamePopup.Visibility = System.Windows.Visibility.Visible;

            closeLoadWindow.Click += closeLoadWindow_Click;

            DatabaseEntities db = new DatabaseEntities();

            IQueryable<Games> query = from entry in db.Games select entry;

            List<Games> x = query.ToList();
            foreach (Games game in x)
            {
                gameNames.Items.Add(game.name);
                //Console.WriteLine(game.name + " -- " + game.Id);
            }

            /*
            databaseEntities db = new databaseEntities();
            IQueryable<test> custQuery =
                from entry in db.test
                select entry;
            List<test> x = custQuery.ToList();
            foreach (test t in x)
                Console.WriteLine(t.Id + "  " + t.name + "   " + t.score);
             * */
            //delete this contact
            /*
            databaseEntities db = new databaseEntities();
            test con = db.test.SingleOrDefault(p => p.Id == 1);
            Console.WriteLine("score = " + con.score);
            db.test.Remove(con);
            db.SaveChanges();*/
        }

        void closeLoadWindow_Click(object sender, RoutedEventArgs e)
        {
            loadGamePopup.Visibility = System.Windows.Visibility.Hidden;
        }

        void closeSaveWindow_Click(object sender, RoutedEventArgs e)
        {
            gameNamePopup.Visibility = System.Windows.Visibility.Hidden;
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
                }
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
