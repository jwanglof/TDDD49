﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class Rules
    {

        private String[] priority = new String[16];

        public Rules()
        {
            priority[0] = ("RoyalFlush");
            priority[1] = ("StraightFlush");
            priority[2] = ("Four");
            priority[3] = ("FullHouseThree");
            priority[4] = ("FullHouseTwo");
            priority[5] = ("Flush");
            priority[6] = ("Straight");
            priority[7] = ("Three");
            priority[8] = ("TwoPairsHigh");
            priority[9] = ("TwoPairsLow");
            priority[10] = ("Pair");
            priority[11] = ("h1");
            priority[12] = ("h2");
            priority[13] = ("h3");
            priority[14] = ("h4");
            priority[15] = ("h5");
        }
        public int checkBestHand(List<Card> player1Cards, List<Card> player2Cards)
        {
            Dictionary<String, int> player1Score = getScore(player1Cards);
            Dictionary<String, int> player2Score = getScore(player2Cards);

            for (int i=0;i<priority.Length;i++)
            {
                String check = priority[i];
                Console.WriteLine("check=" + check);
                if (player1Score[check] > player2Score[check])
                    return 1;
                else if (player1Score[check] < player2Score[check])
                    return 2;

            }
            return 0;
        }

        private Dictionary<String, int> getScore(List<Card> cards)
        {
            Dictionary<String, int> score = new Dictionary<string, int>();
            //RoyalFlush
            score["RoyalFlush"] = checkRoyalFlush(cards);

            //StraightFlush
            score["StraightFlush"] = checkStraightFlush(cards);

            //Four
            score["Four"] = checkFour(cards);

            //FullHouse
            Tuple<int, int> fullHouse = checkFullHouse(cards);
            score["FullHouseThree"] = fullHouse.Item1;
            score["FullHouseTwo"] = fullHouse.Item2;

            //Flush
            score["Flush"] = checkFlush(cards);

            //Straight
            score["Straight"] = checkStraight(cards);

            //Three
            score["Three"] = checkThree(cards);

            //TwoPairs
            Tuple<int, int> twoPairs = checkTwoPairs(cards);
            score["TwoPairsHigh"] = twoPairs.Item1;
            score["TwoPairsLow"] = twoPairs.Item2;

            //Pair
            score["Pair"] = checkPair(cards);

            //HighHand1-2-3-4-5
            cards = cards.OrderByDescending(card => card.getNumber()).ToList();
            for (int i=0;i<cards.Count;i++)
                score["h"+(i+1)] = cards[i].getNumber();

            printScore(score);
            return score;
        }

        /*
         * Prints score to console, for DEBUG ONLY
         */ 
        public void printScore(Dictionary<String, int> score)
        {
            foreach (KeyValuePair<String, int> pair in score)
                Console.WriteLine(pair.Key + " " + pair.Value);
        }

        public int checkRoyalFlush(List<Card> cards)
        {
            if (checkStraightFlush(cards) == 14)
                return cards[0].getSuit();

            return 0;
        }

        public int checkStraightFlush(List<Card> cards)
        {
            int score = checkFlush(cards);
            if ((score != 0) && (checkStraight(cards) != 0))
                    return score;

            return 0;
        }

        public Tuple<int, int> checkFullHouse(List<Card> cards)
        {
            Tuple<int, int> twoPairs = checkTwoPairs(cards); 
            if(twoPairs.Item1 != 0)
            {
                int high = checkThree(cards);
                if (high != 0)
                    if (twoPairs.Item1 == high)
                        return twoPairs;
                    else
                        return new Tuple<int, int>(twoPairs.Item2, twoPairs.Item1);
            }

            return twoPairs;
        }

        public int checkStraight(List<Card> cards)
        {
            cards = cards.OrderBy(card => card.getNumber()).ToList();

            //If there's a ace and a two we should not check the ace as normal
            bool aceAndTwo = ((cards[cards.Count-1].getNumber() == 14) && (cards[1].getNumber() == 2));
            int maxLoop = cards.Count;
            if (aceAndTwo)
                maxLoop -= 1;

            for(int i=1;i<maxLoop;i++)
                if (cards[i - 1].getNumber() != (cards[i].getNumber() - 1))
                    return 0;

            //Ace is lowest in this Straight
            if (aceAndTwo)
                return cards[cards.Count-2].getNumber();

            return cards[cards.Count-1].getNumber(); //Return highest number in Straight
        }

        //Need to have 5 cards in list to use this method
        public int checkFlush(List<Card> cards)
        {
            //Take suit of first card and compare to every other card
            cards = cards.OrderByDescending(card => card.getNumber()).ToList();
            int color = cards[0].getSuit();
            Console.WriteLine("0color="+color);
            for (int i = 1; i < cards.Count; i++)
            {
                Console.WriteLine("color" + i + "=" + cards[i].getSuit());
                if (cards[i].getSuit() != color)
                    return 0;
            }

            return cards[0].getNumber();
        }
        public int checkFour(List<Card> cards)
        {
            //Only need to comapre rest of the cards with the two first cards. If no Four has been found at that point, none will be found.
            cards = cards.OrderByDescending(card => card.getNumber()).ToList();
            if (cards[0].getNumber() == cards[3].getNumber())
                return cards[0].getNumber();
            if (cards[1].getNumber() == cards[4].getNumber())
                return cards[1].getNumber();

            return 0;
        }

        public int checkThree(List<Card> cards)
        {
            cards = cards.OrderByDescending(card => card.getNumber()).ToList();
            for (int i = 2; i < cards.Count; i++)
                if (cards[i - 2].getNumber() == cards[i].getNumber() &&
                    cards[i - 1].getNumber() == cards[i].getNumber())
                    return cards[i].getNumber();

            return 0;
        }

        public int checkPair(List<Card> cards)
        {
            cards = cards.OrderByDescending(card => card.getNumber()).ToList();
            for (int i = 1; i < cards.Count; i++)
                if (cards[i - 1].getNumber() == cards[i].getNumber())
                    return cards[i].getNumber();

            return 0;
        }

        //Returns null if not found, returns highest pair as tuple.item1
        public Tuple<int, int> checkTwoPairs(List<Card> cards)
        {
            int firstPair = checkPair(cards);
            if (firstPair != 0)
            {
                //If a pair was found, remove it from cards
                cards = cards.Where(card => (card.getNumber() != firstPair)).ToList();
                int secondPair = checkPair(cards);
                if(secondPair != 0) 
                    if(firstPair > secondPair)
                        return(new Tuple<int,int>(firstPair,secondPair));
                    else
                        return(new Tuple<int,int>(secondPair,firstPair));   
            }

            return new Tuple<int,int>(0,0);
        }
    }
}
