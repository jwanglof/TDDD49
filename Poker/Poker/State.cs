﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Poker
{
    public class State
    {
        public int id { get; set; }
        public string stateTitle { get; set; }
        public List<Poker.Card> player1Cards { get; set; }
        public List<Poker.Card> player2Cards { get; set; }
        public int player1Money { get; set; }
        public int player2Money { get; set; }
        public State() { }
    }

    public class StateContext : DbContext
    {
        public DbSet<State> States { get; set; }
    }
}
