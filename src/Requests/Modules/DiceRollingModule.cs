using Microsoft.AspNetCore.Builder;
using Requests.Dices;

namespace Requests.Modules
{
    public static class DiceRollingModule
    {
        public static void AddRequests(WebApplication webApplication)
        {
            webApplication.MapPost(RollFacedDiceRequest.EndPoint, RollFacedDiceRequest.Respond);

            webApplication.MapPost(RollFudgeRequest.EndPoint, RollFudgeRequest.Respond);
        }
    }
}
