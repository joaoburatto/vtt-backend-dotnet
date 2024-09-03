using VTT.Database;
using VTT.Requests.Dices.Models;

namespace VTT.Requests.Dices
{
    public class RollFudgeRequest
    {
        public const string EndPoint = "/fudge";

        public static string Respond(HttpContext context)
        {
            string? modifierString = context.Request.Query["modifier"];

            FudgeDice fudgeDice = int.TryParse(modifierString, out int modifier) ? new FudgeDice(modifier) : new FudgeDice(0);

            string json = fudgeDice.ToJson();

            LocalData.SaveWithTimestamp<RollFudgeRequest>(json);
            return json;
        }

    }
}