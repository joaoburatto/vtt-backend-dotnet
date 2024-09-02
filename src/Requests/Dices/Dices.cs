using System.Text.Json;

namespace Requests.Dices
{
    // Static class to normalize usage of random with many dice kinds
    internal static class RandomUtility
    {
        private static readonly Random _random = new Random();
        public static Random Instance => _random;
    }

    public readonly struct FacedDice
    {
        public int Sides { get; }
        public int Modifier { get; }
        public int Roll { get; }
        public int Result { get; }

        public FacedDice(int sides, int modifier = 0)
        {
            Sides = sides;
            Modifier = modifier;

            Roll = RandomUtility.Instance.Next(1, Sides + 1);
            Result = Roll + Modifier;
        }

        public string ToJson()
        {
            var result = new
            {
                Sides,
                Modifier,
                Roll,
                Result
            };

            return JsonSerializer.Serialize(result);
        }
    }

    public readonly struct FudgeDice
    {
	public IReadOnlyList<int> Rolls { get; }
	public int Result { get; }
	public int Modifier { get; }

	public FudgeDice(int modifier = 0)
	{
            var rolls = new List<int>();

	    for (int i = 0; i < 4; ++i)
	    {
                rolls.Add(RandomUtility.Instance.Next(-1, 2));
            }

            Rolls = rolls.AsReadOnly();
            Modifier = modifier;
            Result = Rolls.Sum() + modifier;
        }

	public string ToJson()
	{
            var result = new
            {
                Rolls,
                Result,
                Modifier
            };

            return JsonSerializer.Serialize(result);
        }
    };

    public static class RollFacedDiceRequest
    {
	public const String EndPoint = "/faced";

	public static string Respond(HttpContext context)
	{
            // Extract params
            string? sidesString = context.Request.Query["sides"];
            string? modifierString = context.Request.Query["modifier"];

            // parse with defaults
            int sides = int.TryParse(sidesString, out int sidesValue) ? sidesValue : 20;
            int modifier = int.TryParse(modifierString, out int modifierValue) ? modifierValue : 0;

            FacedDice dice = new FacedDice(sides, modifier);
            return dice.ToJson();
        }
    }

    public static class RollFudgeRequest
    {
	public const String EndPoint = "/fudge";


	public static string Respond(HttpContext context)
	{
            string? modifierString = context.Request.Query["modifier"];

            FudgeDice fudgeDice;

            if (int.TryParse(modifierString, out var modifier))
	    {
                fudgeDice = new FudgeDice(modifier);
            }
	    else
	    {
                fudgeDice = new FudgeDice(0);
            }

            return fudgeDice.ToJson();
        }

    }

    public static class API
    {
        public static void AddRequests(WebApplication webApplication)
        {
            webApplication.MapPost(RollFacedDiceRequest.EndPoint, RollFacedDiceRequest.Respond);

            webApplication.MapPost(RollFudgeRequest.EndPoint, RollFudgeRequest.Respond);
        }
    }

}
