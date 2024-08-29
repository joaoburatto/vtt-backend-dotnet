using System.Text.Json;
using System;

namespace Requests.Dices
{
    [Serializable]
    public class FudgeDice
    {

	public List<int> Rolls {get; set;} = new(); // Vector::new()
	public int Result {get; set;}
	public int Modifier { get; set; }

	public string ToJson()
	{
	    string jsonResult = JsonSerializer.Serialize(this);
	    return jsonResult;
	}

	public FudgeDice Roll()
	{
	    Random random = new Random();

	    for (int i = 0; i < 4; i++)
	    {
		// Runs only in a range of -1 to 1
		// DESPITE the function pointing otherwise. C#.
		int randomInt = random.Next(-1, 2);
		Rolls.Add(randomInt);
	    }

	    Result = Rolls.Sum();

	    return this;
	}

	public FudgeDice AddModifier(int modifier)
	{
	    Modifier = modifier;
	    Result += Modifier;
	    return this;
	}
    }
}
