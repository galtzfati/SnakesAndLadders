using SnakesAndLadders.Enums;
using SnakesAndLadders.Interfaces;

namespace SnakesAndLadders.Models;

internal class Gold : IMover
{
    private readonly Func<IMoveable> _getLeader;

    public int Position { get; }
    public int EndPosition => _getLeader.Invoke().Position;

    public eMover MoverType => eMover.Gold;

    public Gold(int position, Func<IMoveable> getLeader)
    {
        _getLeader = getLeader;
        Position = position;
    }

    public void Move(IMoveable moveable)
    {
        IMoveable leader = _getLeader.Invoke();
        if (leader != moveable)
        {
            (leader.Position, moveable.Position) = (moveable.Position, leader.Position);
        }
    }
}
