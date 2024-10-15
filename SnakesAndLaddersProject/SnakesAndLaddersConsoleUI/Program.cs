using SnakesAndLadders.Enums;
using SnakesAndLadders.GameLogic;
using SnakesAndLadders.Interfaces;
using SnakesAndLadders.Models;

namespace SnakesAndLaddersConsoleUI;

internal class Program
{
    public static void Main(string[] args)
    {
        getNumOfLaddersAndSnakesFromUser(out int numOfSnakes, out int numOfLadders);

        GameBuilder gameBuilder = new GameBuilder();
        gameBuilder.AddPlayer("Rotem")
                   .AddPlayer("Dynamics")
                   .SetLadders(numOfLadders)
                   .SetSnakes(numOfSnakes)
                   .SetGolds(2)
                   .SetBoardSize(10);

        using (Game game = gameBuilder.Build())
        {
            game.PlayerPositionChanged += Game_PlayerPositionChanged;

            displayMoversLocations(game.Movers);

            Console.ReadLine();

            runGame(game);

            declareWinner(game.Winner!);

            game.PlayerPositionChanged -= Game_PlayerPositionChanged;
        }

        Console.ReadLine();
    }
    private static void getNumOfLaddersAndSnakesFromUser(out int numOfSnakes, out int numOfLadders)
    {
        do
        {
            Console.WriteLine("Enter number of snakes: ");
        } while (!int.TryParse(Console.ReadLine(), out numOfSnakes) || numOfSnakes < 0);

        do
        {
            Console.WriteLine("Enter number of ladders: ");
        } while (!int.TryParse(Console.ReadLine(), out numOfLadders) || numOfLadders < 0);
    }
    private static void declareWinner(IPlayer player)
    {
        Console.WriteLine($"WINNER: {player.Name} !!!");
    }
    private static void runGame(Game game)
    {
        int playerNum = 0;
        Dice dice = new Dice(2);

        while (!game.IsOver)
        {
            int diceValue = dice.Roll();
            displayDiceValue(diceValue);

            game.PlayTurn(diceValue);

            playerNum++;
            if (playerNum == game.Players.Count())
            {
                summarizeRound(game.Players);
                playerNum = 0;
            }
        }
    }
    private static void displayDiceValue(int diceValue)
    {
        Console.Write($"Dice: {diceValue} | ");
    }
    private static void displayMoversLocations(IEnumerable<IMover> movers)
    {
        foreach (IMover mover in movers)
        {
            Console.WriteLine($"{mover.MoverType}: ({mover.Position}{(mover.MoverType == eMover.Gold ? string.Empty : $", {mover.EndPosition}")})");
        }
        Console.WriteLine();
    }
    private static void summarizeRound(IEnumerable<IPlayer> players)
    {
        Console.WriteLine("--------------");
        Console.WriteLine("Round Summary:");
        foreach (IPlayer player in players)
        {
            Console.WriteLine($"{player.Name}'s current position: {player.Position}");
        }
        Console.WriteLine("--------------");
        Console.WriteLine();
        Console.ReadLine();
    }
    private static void Game_PlayerPositionChanged(IPlayer player)
    {
        Console.WriteLine($"{player.Name}: ({player.PreviousPosition}) => ({player.Position})");
    }
}