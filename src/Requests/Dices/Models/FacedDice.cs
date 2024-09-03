using System.Text.Json;
using VTT.Utility;

namespace VTT.Requests.Dices.Models
{
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

            Roll = RNG.Random(1, Sides + 1);
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
}