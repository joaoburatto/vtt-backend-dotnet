using VTT.Requests.Dices;

namespace VTT.Requests.Modules
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
