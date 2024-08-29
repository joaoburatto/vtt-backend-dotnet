using System.Text.Json;
using System;

namespace Requests.Dices
{
    public class FacedDice
    {
	public int Sides { get; set; } = 20;
	public int Modifier { get; set; } = 0;
	public int Roll { get; set; } = int.MinValue;
	public int Result { get; set; } = int.MinValue;

	public FacedDice SetSides(string sides)
	{
	    Sides = int.Parse(sides);
	    return this;
	}

	public FacedDice AddModifier(string modifier)
	{
	    Modifier = int.Parse(modifier);
	    return this;
	}

	public FacedDice RollDice()
	{
	    Random random = new Random();
	    // Even tho Sides is the max, it never rolls to the maximum so we add + 1
	    int roll = random.Next(1, Sides + 1);
	    Roll = roll;
	    Result = roll+Modifier; // never negative
	    return this;
	}

	public string ToJson()
	{
	    string jsonResult = JsonSerializer.Serialize(this);
	    return jsonResult;
	}
    }
}
