using SnakesAndLadders.Interfaces;
using SnakesAndLadders.Models;

namespace SnakesAndLadders.GameLogic;

public class GameBuilder : IGameBuilder
{
    private readonly List<IPlayer> _players = new List<IPlayer>();
    private int _boardSize;
    private int _numOfSnakes;
    private int _numOfLadders;
    private int _numOfGolds;

    public GameBuilder AddPlayer(string name)
    {
        _players.Add(new Player(name));
        return this;
    }
    public GameBuilder SetBoardSize(int size)
    {
        _boardSize = size;
        return this;
    }
    public GameBuilder SetSnakes(int numOfSnakes)
    {
        _numOfSnakes = numOfSnakes;
        return this;
    }
    public GameBuilder SetLadders(int numOfLadders)
    {
        _numOfLadders = numOfLadders;
        return this;
    }
    public GameBuilder SetGolds(int numOfGolds)
    {
        _numOfGolds = numOfGolds;
        return this;
    }
    public Game Build()
    {
        List<IMover> movers = new List<IMover>();
        GameEndpointsRandomizer endpointsRandomizer = new GameEndpointsRandomizer(_boardSize);
        Game? game = null;

        for (int i = 0; i < _numOfSnakes; i++)
        {
            movers.Add(new Snake(endpointsRandomizer.CreateUniqueRandomLink()));
        }
        for (int i = 0; i < _numOfLadders; i++)
        {
            movers.Add(new Ladder(endpointsRandomizer.CreateUniqueRandomLink()));
        }
        for (int i = 0; i < _numOfGolds; i++)
        {
            movers.Add(new Gold(endpointsRandomizer.CreateUniqueRandomEndpoint(), () => game!.Players.First(p => p.Position == game.Players.Max(p => p.Position))));
        }

        game = new Game(_players, movers, _boardSize * _boardSize);

        return game;
    }
}
