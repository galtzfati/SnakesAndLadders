using SnakesAndLadders.Enums;
using SnakesAndLadders.Interfaces;

namespace SnakesAndLadders.Models;

internal class Snake : IMover
{
    public eMover MoverType => eMover.Snake;
    public int EndPosition { get; }
    public int Position { get; }

    public Snake(Link<int> endpoints)
    {
        Position = Math.Max(endpoints.Endpoint1, endpoints.Endpoint2);
        EndPosition = Math.Min(endpoints.Endpoint1, endpoints.Endpoint2);
    }

    public void Move(IMoveable moveable)
    {
        moveable.Position = EndPosition;
    }
}
