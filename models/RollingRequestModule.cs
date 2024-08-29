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
        public class FudgeDice
        {

            public List<int> Rolls {get; set;} = new(); // Vector::new()
            public int Result {get; set;}
	    public int Modifier { get; set; }

            public string ToJson()
            {
                string jsonResult = JsonSerializer.Serialize(this);
                return jsonResult;
            }

	    public FudgeDice Roll()
	    {
		Random random = new Random();

		for (int i = 0; i < 4; i++)
		{
		    int randomInt = random.Next(-1, 2);
		    Rolls.Add(randomInt);
		}

                Result = Rolls.Sum();

                return this;
            }

	    public FudgeDice AddModifier(int modifier)
	    {
                Modifier = modifier;
                Result += Modifier;
                return this;
            }
	}

	public static string Respond(HttpContext context)
        {
            FudgeDice result = new FudgeDice();
            result.Roll();

            context.Response.ContentType = "application/json";
            String? modifierString = context.Request.Query["modifier"];
	    if(modifierString != null)
	    {
                var modifier = int.Parse(modifierString);
                result.AddModifier(modifier);
            }

            return result.ToJson();
            // context.Response.WriteAsync(jsonResult).Wait();
        }

    }
}
