using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    static class GlobalVariables
    {
        public static Dictionary<int, String> cardNumbers = new Dictionary<int,string>();
        public static Dictionary<int, String> suitNumbers = new Dictionary<int, string>();
        public static Dictionary<String, String> realCards = new Dictionary<string, string>();

        //public static int numberOfPlayers = 2;
        public static Player player1;
        public static Player player2;

        public static void initGlobalVariables()
        {
            initCardNumbers();
            initCardSuits();
        }

        private static void initCardNumbers() 
        {
            cardNumbers[1] = "1";
            cardNumbers[2] = "2";
            cardNumbers[3] = "3";
            cardNumbers[4] = "4";
            cardNumbers[5] = "5";
            cardNumbers[6] = "6";
            cardNumbers[7] = "7";
            cardNumbers[8] = "8";
            cardNumbers[9] = "9";
            cardNumbers[10] = "10";
            cardNumbers[11] = "j";
            cardNumbers[12] = "q";
            cardNumbers[13] = "k";
        }

        private static void initCardSuits()
        {
            /*suitNumbers[1] = "Spades";
            suitNumbers[2] = "Clubs";
            suitNumbers[3] = "Diamonds";
            suitNumbers[4] = "Hearts";*/
            suitNumbers[1] = "s";
            suitNumbers[2] = "c";
            suitNumbers[3] = "d";
            suitNumbers[4] = "h";
        }

        private static void initRealCards()
        {
            
        }

        public static String toCardValue(int number)
        {
            return cardNumbers[number];
        }

        public static String toCardSuit(int suit)
        {
            return suitNumbers[suit];
        }
    }
}
