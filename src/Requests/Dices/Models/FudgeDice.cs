using System.Text.Json;
using VTT.Utility;

namespace VTT.Requests.Dices.Models
{
    public readonly struct FudgeDice
    {
        private List<int> Rolls { get; }
        private int Result { get; }
        private int Modifier { get; }

        public FudgeDice(int modifier = 0)
        {
            var rolls = new List<int>();

            for (int i = 0; i < 4; ++i)
            {
                int random = RNG.Random(-1, 2);

                rolls.Add(random);
            }

            Rolls = rolls;
            Modifier = modifier;
            Result = Rolls.Sum() + modifier;
        }

        public string ToJson()
        {
            string json = JsonSerializer.Serialize(new
            {
                Rolls,
                Result,
                Modifier
            });

            return json;
        }
    };
}