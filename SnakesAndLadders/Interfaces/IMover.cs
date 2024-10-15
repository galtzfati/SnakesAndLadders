using SnakesAndLadders.Enums;

namespace SnakesAndLadders.Interfaces;

public interface IMover : IPositionable
{
    int EndPosition { get; }
    eMover MoverType { get; }
    void Move(IMoveable moveable);
}