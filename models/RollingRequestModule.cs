using System;
using Microsoft.AspNetCore.Builder;
using System.Text.Json;

namespace Requests.Utility
{
    public static class RollingRequestModule
    {
        public static void AddRequests(WebApplication webApplication)
        {
            webApplication.MapPost(RollDiceRequest.EndPoint, RollDiceRequest.Respond);

            webApplication.MapPost(RollFudgeRequest.EndPoint, RollFudgeRequest.Respond);
        }
    }

    [Serializable]
    public static class RollDiceRequest
    {
        public const String EndPoint = "/d20";

        public static int Respond(HttpContext context)
        {
            Random random = new Random();

            String? sidesString = context.Request.Query["sides"];
            if (sidesString == null)
            {
                sidesString = "20";
            }

            int sidesValue = int.Parse(sidesString);

            int randomInt = random.Next(1, sidesValue);

            return randomInt;
        }
    }

    public static class RollFudgeRequest
    {
        public const String EndPoint = "/fudge";

	[Serializable]
        public class FudgeResult
        {
            public List<int> Rolls {get; set;} = new List<int>();
            public int Result {get; set;}
        }

        public static string Respond(HttpContext context)
        {
            FudgeResult result = new FudgeResult
            {
                Rolls = new List<int>()
            };

            Random random = new Random();

            for (int i = 0; i < 4; i++)
            {
                int randomInt = random.Next(-1, 2);
                result.Rolls.Add(randomInt);
            }

            result.Result = result.Rolls.Sum();

            string jsonResult = JsonSerializer.Serialize(result);
            context.Response.ContentType = "application/json";

            return jsonResult;
            // context.Response.WriteAsync(jsonResult).Wait();
        }

    }
}
