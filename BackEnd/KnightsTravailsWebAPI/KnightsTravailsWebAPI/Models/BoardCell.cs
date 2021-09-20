using System;

namespace KnightsTravailsWebAPI.Models
{
    public class BoardCell
    {
        public int x;
        public int y;

        public Tuple<int, int> Coordinates => Tuple.Create(x, y);

        public BoardCell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public BoardCell(Tuple<int, int> coordinates)
        {
            x = coordinates.Item1;
            y = coordinates.Item2;
        }
    }
}