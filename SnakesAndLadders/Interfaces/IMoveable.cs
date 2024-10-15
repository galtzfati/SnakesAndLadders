namespace SnakesAndLadders.Interfaces;

public interface IMoveable : IPositionable
{
    int PreviousPosition { get; }
    new int Position { get; set; }
}
