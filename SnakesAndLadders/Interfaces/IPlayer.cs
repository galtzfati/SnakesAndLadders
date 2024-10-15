namespace SnakesAndLadders.Interfaces;

public interface IPlayer : IMoveable
{
    string Name { get; }
    event Action<IPlayer>? PositionChanged;
}
