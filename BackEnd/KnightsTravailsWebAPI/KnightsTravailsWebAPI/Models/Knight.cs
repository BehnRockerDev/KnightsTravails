using System;
using System.Collections.Generic;

namespace KnightsTravailsWebAPI.Models
{
    class Knight : ChessPiece
    {
        //A knight moves in an L-path (2 squares away from start, a 90 degree turn, and then 1 more square in the direction turned.
        //A knight can move to one of 8 potential squares any time it moves.
        //This list represents the potential ways the piece will move in the (x, y) axis, from it's start location.
        private readonly List<Tuple<int, int>> PotentialKnightPaths = new List<Tuple<int, int>>
        {
            Tuple.Create(1, 2),
            Tuple.Create(1, -2),
            Tuple.Create(-1, 2),
            Tuple.Create(-1, -2),
            Tuple.Create(2, 1),
            Tuple.Create(2, -1),
            Tuple.Create(-2, 1),
            Tuple.Create(-2, -1)
        };

        //Overriding the parent classes PotentialMoves
        public override List<Tuple<int, int>> PotentialMoves
        {
            get { return PotentialKnightPaths; }
        }

    }
}