using KnightsTravailsWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KnightsTravailsWebAPI.Controllers
{
    public class ChessController : ApiController
    {
        private KnightSolver solver;

        public ChessController()
        {
            solver = new KnightSolver();
        }

        [Route("api/Chess/GetShortestPath/{startPosition}/{endPosition}")]
        [HttpGet]
        public List<string> GetShortestPath(string startPosition, string endPosition) 
            => solver.GetShortestPathToDestinationInChessNotation(startPosition, endPosition);

    }
}
