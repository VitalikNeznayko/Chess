using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class GameStateChangedEventArgs : EventArgs
    {
        public bool IsWhiteTurn { get; set; }
        public bool IsCheck { get; set; }
        public bool IsGameEnded { get; set; }
        public string LastMove { get; set; }
    }
}
