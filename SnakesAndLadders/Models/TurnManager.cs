namespace SnakesAndLadders.Models;

internal class TurnManager<T>
{
    private readonly List<T> _values;
    private int _turn = 0;

    public TurnManager(List<T> values)
    {
        _values = values;
    }

    public T Next
    {
        get
        {
            T value = _values[_turn];
            _turn = (_turn + 1) % _values.Count;
            return value;
        }
    }
}
