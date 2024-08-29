using Microsoft.AspNetCore.Builder;

namespace Requests.Dices
{
    [Serializable]
    public static class RollFacedDiceRequest
    {
	public const String EndPoint = "/faced";

	public static string Respond(HttpContext context)
	{
	    FacedDice dice = new();

	    // Take from query request
	    String? sidesString = context.Request.Query["sides"];
	    String? modifierString = context.Request.Query["modifier"];

	    if (sidesString != null)
	    {
		dice.SetSides(sidesString);
	    }

	    if (modifierString != null)
	    {
		dice.AddModifier(modifierString);
	    }

	    return dice.RollDice().ToJson();
	}
    }
}
