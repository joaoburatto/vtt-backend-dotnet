using System;
using Microsoft.AspNetCore.Builder;

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
	public struct FudgeResult
	{
            public List<int> Rolls;
            public int Result;
        }

	public static FudgeResult Respond(HttpContext context)
	{
            FudgeResult result = new FudgeResult();
            result.Rolls = new List<int>();

            Random random = new Random();

            for (int i = 0; i < 3; i++)
	    {
		int randomInt = random.Next(-1, 1);
		result.Rolls.Add(randomInt);
	    }

            for (int i = 0; i < 3; i++)
	    {
                result.Result += result.Rolls[i];
            }

            return result;
        }

    }
}
