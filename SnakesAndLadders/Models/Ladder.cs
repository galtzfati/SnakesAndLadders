using SnakesAndLadders.Enums;
using SnakesAndLadders.Interfaces;

namespace SnakesAndLadders.Models;

internal class Ladder : IMover
{
    public int Position { get; }
    public int EndPosition { get; }

    public eMover MoverType => eMover.Ladder;

    public Ladder(Link<int> endpoints)
    {
        Position = Math.Min(endpoints.Endpoint1, endpoints.Endpoint2);
        EndPosition = Math.Max(endpoints.Endpoint1, endpoints.Endpoint2);
    }

    public void Move(IMoveable moveable)
    {
        moveable.Position = EndPosition;
    }
}
