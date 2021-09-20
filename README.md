# KnightsTravails ASP.NET Web API

## Description

>NOTE: This solution needs to be run on Windows. This is not a cross-platform solution.

To run this, you will need to open Visual Studio 2019, and open up the solution found in `BackEnd\KnightsTravailsWebAPI`

Hitting the run button in VS2019 will deploy and run the back end on localhost (http:/ localhost:44328). There is only one endpoint to hit on this API, which is named `/api/chess/getshortestpath/VAR1/VAR2`

Getshortestpath is set up to take in two parameters (in chess notation). It will then return an XML or JSON string list of the shortest path a knight will take between the two parameter values. This can be tested by visiting the URL in a browser (or tool like Postman), and replacing VAR1/VAR2 with a start/end position in chess notation.

The logic is written C#. The knight’s path is determined using a breadth-first search (BFM). This is a branching search which will find and return the shortest possibly path from one location to another. After finding the right path, it will return a list of strings in chess notation, which shows the path to take from start-to-finish.
