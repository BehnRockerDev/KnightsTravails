using System;
using System.Collections.Generic;
using System.Linq;

namespace KnightsTravailsWebAPI.Models
{
    class ChessBrain
    {
        private readonly int _maxRows = 8;
        private readonly int _maxColumns = 8;
        private readonly int _minRows = 1;
        private readonly int _minColumns = 1;

        public Tuple<int, int> ChessNotationToTuple(string chessNotationPosition)
        {
            var sanitizedPosition = SanitizeChessNotation(chessNotationPosition);

            if (!IsValidChessNotation(sanitizedPosition))
            {
                throw new Exception($"{sanitizedPosition} is not in valid chess notation.");
            }

            var column = sanitizedPosition[0].ToString();
            int row = Int32.Parse(sanitizedPosition[1].ToString());

            var coordinates = Tuple.Create(row, ColumnLetterToNumber(column));

            if (!CoordinatesAreInBounds(coordinates))
            {
                throw new Exception($"Coordinate {coordinates} is not in bounds");
            }

            return coordinates;
        }

        private int ColumnLetterToNumber(string columnLetter)
        {
            switch (columnLetter)
            {
                case "A":
                    return 1;
                case "B":
                    return 2;
                case "C":
                    return 3;
                case "D":
                    return 4;
                case "E":
                    return 5;
                case "F":
                    return 6;
                case "G":
                    return 7;
                case "H":
                    return 8;
                default:
                    throw new Exception($"Column letter '{columnLetter}' is out of bounds");
            }
        }

        private string ColumnNumberToLetter(int columnNumber)
        {
            switch (columnNumber)
            {
                case 1:
                    return "A";
                case 2:
                    return "B";
                case 3:
                    return "C";
                case 4:
                    return "D";
                case 5:
                    return "E";
                case 6:
                    return "F";
                case 7:
                    return "G";
                case 8:
                    return "H";
                default:
                    throw new Exception($"Column number '{columnNumber}' is out of bounds");
            }
        }

        private bool CoordinatesAreInBounds(Tuple<int, int> coordinates)
        {
            var row = coordinates.Item1;
            var col = coordinates.Item2;


            if (!Enumerable.Range(_minRows, _maxRows).Contains(row))
            {
                return false;
            }

            if (!Enumerable.Range(_minColumns, _maxColumns).Contains(col))
            {
                return false;
            }

            return true;
        }

        private List<Tuple<int, int>> GetAllCurrentPotentialMoves(List<Tuple<int, int>> offsetList, Tuple<int, int> startPosition)
        {
            var moveList = new List<Tuple<int, int>>();

            foreach (var offset in offsetList)
            {
                moveList.Add(Tuple.Create((startPosition.Item1 + offset.Item1), (startPosition.Item2 + offset.Item2)));
            }

            return moveList;
        }

        private List<Tuple<int, int>> GetValidMoves(ChessPiece piece, Tuple<int, int> startPosition) 
            => GetValidMovesFromList(GetAllCurrentPotentialMoves(piece.PotentialMoves, startPosition));

        public List<BoardCell> GetValidMoveCells(ChessPiece piece, Tuple<int, int> startPosition)
        {
            var cellList = new List<BoardCell>();

            foreach (var move in GetValidMoves(piece, startPosition))
            {
                cellList.Add(new BoardCell(move));
            }

            return cellList;
        }

        private List<Tuple<int, int>> GetValidMovesFromList(List<Tuple<int, int>> allMoves)
        {
            allMoves.RemoveAll(x => !CoordinatesAreInBounds(x));
            return allMoves;
        }

        public List<string> GetValidMovesInChessNotation(ChessPiece piece, Tuple<int, int> startPosition)
        {
            var validMoves = GetValidMoves(piece, startPosition);
            var validMovesInChessNotation = new List<string>();

            foreach (var move in validMoves)
            {
                validMovesInChessNotation.Add(TupleToChessNotation(move));
            }

            return validMovesInChessNotation;

        }

        private bool IsValidChessNotation(string chessNotationPosition)
        {
            //Makes sure that the chess notation isn't too short or too long.
            if (chessNotationPosition.Length > 2)
            {
                return false;
            }
            else if (chessNotationPosition.Length < 2)
            {
                return false;
            }

            //Makes sure first character is a char, and second character is a number
            if (!Char.IsLetter(chessNotationPosition[0]) || !Char.IsNumber(chessNotationPosition[1]))
            {
                return false;
            }

            return true;
        }

        private string SanitizeChessNotation(string chessNotationPosition) => chessNotationPosition.ToUpper().Trim();

        public string TupleToChessNotation(Tuple<int, int> position) => $"{ColumnNumberToLetter(position.Item2)}{position.Item1}";

    }
}