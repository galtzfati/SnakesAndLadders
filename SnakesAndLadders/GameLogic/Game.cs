using SnakesAndLadders.Interfaces;
using SnakesAndLadders.Models;

namespace SnakesAndLadders.GameLogic;

public class Game : IDisposable
{
    private readonly Dictionary<int, IMover> _positionToMover;
    private readonly int _winningPosition;
    private readonly TurnManager<IPlayer> _turnManager;

    public bool IsOver { get; private set; } = false;
    public IPlayer? Winner { get; private set; }
    public IEnumerable<IPlayer> Players { get; }
    public IEnumerable<IMover> Movers { get; }
    public event Action<IPlayer>? PlayerPositionChanged;

    public Game(IEnumerable<IPlayer> players,
                IEnumerable<IMover> movers,
                int winningPosition)
    {
        Players = players;
        Movers = movers;
        _turnManager = new TurnManager<IPlayer>(players.ToList());
        subscribeToPlayersPositionChangedEvent();
        _positionToMover = getPositionToMoverMapping(movers);
        _winningPosition = winningPosition;
    }

    public void PlayTurn(int diceValue)
    {
        if (!IsOver)
        {
            IPlayer player = _turnManager.Next;
            player.Position = Math.Clamp(player.Position + diceValue, 0, _winningPosition);
            if (player.Position == _winningPosition)
            {
                IsOver = true;
                Winner = player;
            }
            else if (_positionToMover.ContainsKey(player.Position))
            {
                _positionToMover[player.Position].Move(player);
            }
        }
    }
    public void Dispose()
    {
        unsubscribeToPlayersPositionChangedEvent();
    }
    private void unsubscribeToPlayersPositionChangedEvent()
    {
        foreach (IPlayer player in Players)
        {
            player.PositionChanged -= Player_PositionChanged;
        }
    }
    private void subscribeToPlayersPositionChangedEvent()
    {
        foreach (IPlayer player in Players)
        {
            player.PositionChanged += Player_PositionChanged;
        }
    }
    private void Player_PositionChanged(IPlayer player)
    {
        PlayerPositionChanged?.Invoke(player);
    }
    private Dictionary<int, IMover> getPositionToMoverMapping(IEnumerable<IMover> movers)
    {
        Dictionary<int, IMover> positionToMovers = new Dictionary<int, IMover>();
        foreach (IMover mover in movers)
        {
            if (!positionToMovers.ContainsKey(mover.Position))
            {
                positionToMovers[mover.Position] = mover;
            }
            else
            {
                throw new InvalidOperationException("Each IMover should be located on a unique position!");
            }
        }
        return positionToMovers;
    }
}
