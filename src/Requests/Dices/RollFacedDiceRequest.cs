using VTT.Requests.Dices.Models;

namespace VTT.Requests.Dices
{
    public static class RollFacedDiceRequest
    {
        public const string EndPoint = "/faced";

        public static string Respond(HttpContext context)
        {
            string? sidesString = context.Request.Query["sides"];
            string? modifierString = context.Request.Query["modifier"];

            int sides = int.TryParse(sidesString, out int sidesValue) ? sidesValue : 20;
            int modifier = int.TryParse(modifierString, out int modifierValue) ? modifierValue : 0;

            FacedDice dice = new FacedDice(sides, modifier);
            return dice.ToJson();
        }
    }
}