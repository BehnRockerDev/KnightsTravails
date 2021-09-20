using System;
using System.Collections.Generic;
using System.Linq;

namespace KnightsTravailsWebAPI.Models
{
    public class KnightSolver
    {
        private readonly ChessBrain chessBrain;
        private readonly Knight knight;
        private Tuple<int, int> startPosition;
        private Tuple<int, int> endPosition;

        public KnightSolver()
        {
            chessBrain = new ChessBrain();
            knight = new Knight();
        }

        public List<string> GetShortestPathToDestinationInChessNotation(string startPosition, string endPosition)
        {
            var pathAsChessNotation = new List<string>();
            var shortestPath = GetShortestPathToDestination(startPosition, endPosition);

            foreach (var cell in shortestPath)
            {
                pathAsChessNotation.Add(chessBrain.TupleToChessNotation(cell.Coordinates));
            }

            return pathAsChessNotation;
        }

        private List<BoardCell> GetShortestPathToDestination(string start, string end)
        {
            startPosition = chessBrain.ChessNotationToTuple(start);
            endPosition = chessBrain.ChessNotationToTuple(end);

            //Queue made up of potential solutions.
            //Stores the state of the knight on the board
            Queue<List<BoardCell>> queue = new Queue<List<BoardCell>>();

            //List of cells that have been visited
            List<BoardCell> visitedCells = new List<BoardCell>();

            //First solution to try. This is just the start position.
            List<BoardCell> startSolution = new List<BoardCell>();
            var startCell = new BoardCell(startPosition);
            startSolution.Add(startCell);

            //Pushing to the queue.
            queue.Enqueue(startSolution);
            visitedCells.Add(startCell);

            while (queue.Count != 0)
            {
                //Gets the partially completed path at the top of the queue
                List<BoardCell> currentSolution = queue.Dequeue();

                BoardCell currentCell = currentSolution.Last();

                //If currentCell's coordinates match the endPosition, the list of coordinates is returned.
                //The way BFS works ensures that this is the shortest path.
                if (currentCell.x == endPosition.Item1 && currentCell.y == endPosition.Item2)
                {
                    return currentSolution;
                }

                foreach (var cell in chessBrain.GetValidMoveCells(knight, currentCell.Coordinates))
                {
                    //Find all cells that the piece can move to from the current cell
                    if (!visitedCells.Any(x => x.Coordinates == cell.Coordinates))
                    {
                        //If a cell that can be moved to hasn't been visited, it branches
                        //Creates a new partially completed solution, and adds to queue.
                        List<BoardCell> branch = new List<BoardCell>();
                        branch.AddRange(currentSolution);
                        branch.Add(cell);

                        visitedCells.Add(cell);

                        queue.Enqueue(branch);
                    }
                }
            }

            return new List<BoardCell>();
        }
    }
}