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
        private List<Games> listOfGames;
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
        }

        public void onClickLoad(object sender, RoutedEventArgs e)
        {
            // Toggle visibility on the board's button
            if (loadGamePopup.Visibility == System.Windows.Visibility.Visible)
                loadGamePopup.Visibility = System.Windows.Visibility.Hidden;
            else
                loadGamePopup.Visibility = System.Windows.Visibility.Visible;

            loadGame.Click += loadGame_Click;
            closeLoadWindow.Click += closeLoadWindow_Click;

            DatabaseEntities db = new DatabaseEntities();

            IQueryable<Games> query = from entry in db.Games select entry;

            listOfGames = query.ToList();
            Console.WriteLine(listOfGames.Count);
            foreach (Games game in listOfGames)
            {
                gameNames.Items.Add(game.name);
            }
        }

        void loadGame_Click(object sender, RoutedEventArgs e)
        {
            string gameName = (string) gameNames.SelectedItem;

            DatabaseEntities db2 = new DatabaseEntities();

            IQueryable<Games> query = from entry in db2.Games where entry.name == gameName select entry;
            List<Games> loadedGame = query.ToList();
            foreach (Games g in loadedGame)
            {
                game.parseGameEntity(listOfGames[g.Id-1]);
            }

            loadGamePopup.Visibility = System.Windows.Visibility.Hidden;
        }

        void closeLoadWindow_Click(object sender, RoutedEventArgs e)
        {
            loadGamePopup.Visibility = System.Windows.Visibility.Hidden;
        }

        void closeSaveWindow_Click(object sender, RoutedEventArgs e)
        {
            gameNamePopup.Visibility = System.Windows.Visibility.Hidden;
        }

        private void saveGame_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("before");
            Games gameEntity = game.getEntity();

            gameEntity.name = stateName.Text;

            DatabaseEntities db = new DatabaseEntities();
            db.Games.Add(gameEntity);
            db.SaveChanges();
            Console.WriteLine("Save done!");

            /*IQueryable<Games> custQuery =
                from entry in db.Games
                select entry;
            List<Games> x = custQuery.ToList();
            foreach (Games t in x)
                Console.WriteLine(t.Id + "  " + t.name);*/

            gameNamePopup.Visibility = System.Windows.Visibility.Hidden;
            stateName.Text = "";
        }
    }
}
