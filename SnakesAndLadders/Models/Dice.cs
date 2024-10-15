namespace SnakesAndLadders.Models;

public class Dice
{
    private readonly int _numOfDiceToUse;
    private readonly Random _random = new Random();
    private readonly int _maxDiceValue;
    private readonly int _minDiceValue;
    private const int MaxDiceValue = 6;
    private const int MinDiceValue = 1;

    public Dice(int numOfDiceToUse, int maxDiceValue = MaxDiceValue,
                int minDiceValue = MinDiceValue)
    {
        _numOfDiceToUse = numOfDiceToUse;
        _maxDiceValue = maxDiceValue;
        _minDiceValue = minDiceValue;
    }

    public int Roll()
    {
        int value = 0;
        for (int i = 0; i < _numOfDiceToUse; i++)
        {
            value += _random.Next(_minDiceValue, _maxDiceValue + 1);
        }
        return value;
    }
}
