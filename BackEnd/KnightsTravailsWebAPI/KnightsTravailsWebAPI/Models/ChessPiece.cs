using System;
using System.Collections.Generic;

namespace KnightsTravailsWebAPI.Models
{
    abstract class ChessPiece
    {
        public abstract List<Tuple<int, int>> PotentialMoves { get; }
    }
}