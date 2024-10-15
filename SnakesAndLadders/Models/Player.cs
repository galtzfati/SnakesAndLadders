using SnakesAndLadders.Interfaces;

namespace SnakesAndLadders.Models;

internal class Player : IPlayer
{
    public string Name { get; }

    public event Action<IPlayer>? PositionChanged;

    private int _position;
    public int Position
    {
        get => _position;
        set
        {
            PreviousPosition = _position;
            _position = value;
            onPositionChanged();
        }
    }
    public int PreviousPosition { get; private set; }   

    public Player(string name)
    {
        Name = name;
    }

    private void onPositionChanged()
    {
        PositionChanged?.Invoke(this);
    }
}