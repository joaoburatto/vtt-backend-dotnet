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

    // [] D100
    // [x] D20
    // [ ] D12
    // [ ] D10
    // [ ] D8
    // [ ] D6
    // [ ] D4
    // [ ] D2

    [Serializable]
    public static class RollDiceRequest
    {
        public const String EndPoint = "/faced";

	public class DiceResult
	{

            public int Sides { get; set; } = 20;
            public int Modifier { get; set; } = 0;
            public int Roll { get; set; } = int.MinValue;
            public int Result { get; set; } = int.MinValue;

	    public DiceResult SetSides(string sides)
	    {
		Sides = int.Parse(sides);
                return this;
            }

	    public DiceResult AddModifier(string modifier)
	    {
                Modifier = int.Parse(modifier);
                return this;
            }

	    public DiceResult RollDice()
	    {
		Random random = new Random();
		int roll = random.Next(1, Sides);
                Roll = roll;
                Result = roll+Modifier; // never negative
                return this;
            }

	    public string ToJson()
	    {
                string jsonResult = JsonSerializer.Serialize(this);
                return jsonResult;
	    }
	}

        public static string Respond(HttpContext context)
        {
            DiceResult dice = new();

            IQueryCollection? query = context.Request.Query;
            if (query["sides"] != QueryString.Empty)
            {
                dice.SetSides(query["sides"]!);
            }

	    if (query["modifier"] != QueryString.Empty)
	    {
                dice.AddModifier(query["modifier"]!);
            }

            return dice.RollDice().ToJson();
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
