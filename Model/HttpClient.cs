using RestSharp;
using RestSharp.Serialization.Json;
using System;
using UnityEngine;

namespace Assets.scripts.Model
{
    class HttpClient
    {
        private const String url = "https://chessblunders.org/api/";
        private const String puzzleResource = "blunder/get";
        private const String validationResource = "blunder/validate";
        private static RestClient Client;
        private static Puzzle lastPuzzle;

        // Singleton pattern
        private static RestClient getRestClient()
        {
            if(Client == null)
                Client = new RestClient(url);
            return Client;
        }

        public Puzzle retrieveRandomPuzzle()
        {
            if (lastPuzzle != null)
                sendValidation();

            RestClient cl = getRestClient();

            RestRequest request = new RestRequest(puzzleResource);
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(new { type = "explore" }); // Serializes object to JSON

            var response = cl.Post(request);

            if(response.ResponseStatus == ResponseStatus.Completed)
            {
                JsonSerializer js = new JsonSerializer();
                js.RootElement = "data";
                lastPuzzle = js.Deserialize<Puzzle>(response);
                return lastPuzzle;
            }

            return null;

        }

        // Before the serverside gives a new puzzle it wants the know the players move.
        // This method sends the serverside the solution so it can keep sending new puzzles.
        public void sendValidation()
        {
            if (lastPuzzle == null)
                return;

            RestClient cl = getRestClient();
            RestRequest request = new RestRequest(validationResource);
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(new
            {
                id = lastPuzzle.Id,
                line = lastPuzzle.ForcedLine,
                spentTime = 10,
                type = "explore"
            });

            cl.Post(request);
        }

    }
}
