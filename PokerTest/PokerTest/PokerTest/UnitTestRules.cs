using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker;
using System.Collections.Generic;


namespace PokerTest
{
    [TestClass]
    public class UnitTestRules
    {
        private List<Card> noScore = new List<Card>();

        public UnitTestRules()
        {
            noScore.Add(new Card(2, 1, true));
            noScore.Add(new Card(3, 2, true));
            noScore.Add(new Card(9, 3, true));
            noScore.Add(new Card(4, 2, true));
            noScore.Add(new Card(7, 2, true));
        }
        [TestMethod]
        public void TestRoyalFlush()
        {
            Rules rules = new Rules();
            List<Card> cards = new List<Card>();
            cards.Add(new Card(14, 1, true));
            cards.Add(new Card(13, 1, true));
            cards.Add(new Card(12, 1, true));
            cards.Add(new Card(11, 1, true));
            cards.Add(new Card(10, 1, true));

            Assert.IsTrue(rules.checkRoyalFlush(cards) != 0);
            Assert.IsTrue(rules.checkRoyalFlush(noScore) == 0);
        }

        [TestMethod]
        public void TestStraightFlush()
        {
            Rules rules = new Rules();
            List<Card> cards = new List<Card>();
            cards.Add(new Card(10, 2, true));
            cards.Add(new Card(9, 2, true));
            cards.Add(new Card(8, 2, true));
            cards.Add(new Card(7, 2, true));
            cards.Add(new Card(6, 2, true));

            Assert.IsTrue(rules.checkStraightFlush(cards) == 10);
            Assert.IsTrue(rules.checkStraightFlush(noScore) == 0);
        }

        [TestMethod]
        public void TestFour()
        {
            Rules rules = new Rules();
            List<Card> cards = new List<Card>();
            cards.Add(new Card(5, 4, true));
            cards.Add(new Card(5, 3, true));
            cards.Add(new Card(5, 2, true));
            cards.Add(new Card(5, 1, true));
            cards.Add(new Card(4, 4, true));

            Assert.IsTrue(rules.checkFour(cards) == 5);
            Assert.IsTrue(rules.checkFour(noScore) == 0);
        }

        [TestMethod]
        public void TestFullHouse()
        {
            Rules rules = new Rules();
            List<Card> cards = new List<Card>();
            cards.Add(new Card(10, 4, true));
            cards.Add(new Card(10, 3, true));
            cards.Add(new Card(10, 2, true));
            cards.Add(new Card(5, 1, true));
            cards.Add(new Card(5, 4, true));

            Assert.IsTrue(rules.checkFullHouse(cards).Item1 == 10);
            Assert.IsTrue(rules.checkFullHouse(cards).Item2 == 5);

            Assert.IsTrue(rules.checkFullHouse(noScore).Item1 == 0);
            Assert.IsTrue(rules.checkFullHouse(noScore).Item2 == 0);
        }

        [TestMethod]
        public void TestFlush()
        {
            Rules rules = new Rules();
            List<Card> cards = new List<Card>();
            cards.Add(new Card(2, 1, true));
            cards.Add(new Card(5, 1, true));
            cards.Add(new Card(8, 1, true));
            cards.Add(new Card(9, 1, true));
            cards.Add(new Card(11, 1, true));

            Assert.IsTrue(rules.checkFlush(cards) == 11);
            Assert.IsTrue(rules.checkFlush(noScore) == 0);
        }

        [TestMethod]
        public void TestStraight()
        {
            Rules rules = new Rules();
            List<Card> cards = new List<Card>();
            cards.Add(new Card(2, 1, true));
            cards.Add(new Card(3, 1, true));
            cards.Add(new Card(4, 2, true));
            cards.Add(new Card(5, 4, true));
            cards.Add(new Card(6, 3, true));

            Assert.IsTrue(rules.checkStraight(cards) == 6);
            Assert.IsTrue(rules.checkStraight(noScore) == 0);
        }

        [TestMethod]
        public void TestThree()
        {
            Rules rules = new Rules();
            List<Card> cards = new List<Card>();
            cards.Add(new Card(14, 4, true));
            cards.Add(new Card(14, 3, true));
            cards.Add(new Card(14, 2, true));
            cards.Add(new Card(5, 3, true));
            cards.Add(new Card(4, 4, true));

            Assert.IsTrue(rules.checkThree(cards) == 14);
            Assert.IsTrue(rules.checkThree(noScore) == 0);
        }

        [TestMethod]
        public void TestTwoPairs()
        {
            Rules rules = new Rules();
            List<Card> cards = new List<Card>();
            cards.Add(new Card(12, 4, true));
            cards.Add(new Card(12, 3, true));
            cards.Add(new Card(9, 2, true));
            cards.Add(new Card(14, 1, true));
            cards.Add(new Card(14, 4, true));

            Assert.IsTrue(rules.checkTwoPairs(cards).Item1 == 14);
            Assert.IsTrue(rules.checkTwoPairs(cards).Item2 == 12);

            Assert.IsTrue(rules.checkTwoPairs(noScore).Item1 == 0);
            Assert.IsTrue(rules.checkTwoPairs(noScore).Item2 == 0);
        }

        [TestMethod]
        public void TestPair()
        {
            Rules rules = new Rules();
            List<Card> cards = new List<Card>();
            cards.Add(new Card(13, 4, true));
            cards.Add(new Card(13, 2, true));
            cards.Add(new Card(10, 2, true));
            cards.Add(new Card(3, 3, true));
            cards.Add(new Card(4, 4, true));

            Assert.IsTrue(rules.checkPair(cards) == 13);
            Assert.IsTrue(rules.checkPair(noScore) == 0);
        }
    }
}
