using System.Text.Json;

namespace Requests.Dices
{
    public static class RollFudgeRequest
    {
	public const String EndPoint = "/fudge";


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
