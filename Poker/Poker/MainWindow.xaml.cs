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

            // Initialize the global variables
            GlobalVariables.initGlobalVariables();
            GlobalVariables.player1 = player1;
            GlobalVariables.player2 = player2;

            // Start a new game
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

            // Clear the window where all the saved games are shown
            gameNames.Items.Clear();

            // Add button click events
            loadGame.Click += loadGame_Click;
            closeLoadWindow.Click += closeLoadWindow_Click;

            // Load the saved games and populate the gameNames-ListBox
            DatabaseEntities db = new DatabaseEntities();
            IQueryable<Games> query = from entry in db.Games select entry;
            listOfGames = query.ToList();
            foreach (Games game in listOfGames)
            {
                gameNames.Items.Add(game.name);
            }
        }

        void loadGame_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected game name
            string gameName = (string) gameNames.SelectedItem;

            DatabaseEntities load_db = new DatabaseEntities();
            IQueryable<Games> query = from entry in load_db.Games where entry.name == gameName select entry;
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
            // Get the entity
            Games gameEntity = game.getEntity();

            // Set the game name to whatever the user named it
            gameEntity.name = stateName.Text;

            DatabaseEntities db = new DatabaseEntities();
            db.Games.Add(gameEntity);
            db.SaveChanges();

            /*textMessage.Text = "Saved game!";
            textMessage.Visibility = System.Windows.Visibility.Visible;

            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += aTimer_Elapsed;
            aTimer.AutoReset = false;
            aTimer.Enabled = true;*/

            gameNamePopup.Visibility = System.Windows.Visibility.Hidden;
            stateName.Text = "";
        }
    }
}
