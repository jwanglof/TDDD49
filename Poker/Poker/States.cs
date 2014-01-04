using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class States
    {
        public States() { }

        public int stateId { get; set; }
        public string stateTitle { get; set; }
        public List<Poker.Card> player1Cards { get; set; }
        public List<Poker.Card> player2Cards { get; set; }
        public int player1Money { get; set; }
        public int player2Money { get; set; }
    }

    public struct saveGameData
    {
        public string gameName;
    }
}
