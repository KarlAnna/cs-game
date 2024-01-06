using Classes;

class Program
{
  static void Main()
  {
    Run();
  }

  static void Run()
  {
    GameAccountBase player1 = new StandardGameAccount("Player 1", Constants.MinRating);
    GameAccountBase player2 = new StandardGameAccount("Player 2", Constants.MinRating);

    GameFactory gameFactory = new GameFactory();

    player1.WinGame(gameFactory.CreateGame(GameType.Standard, player2.UserName, GameResult.Win));
    player2.LoseGame(gameFactory.CreateGame(GameType.Standard, player1.UserName, GameResult.Loss));

    player1.WinGame(gameFactory.CreateGame(GameType.DoublePoints, player2.UserName, GameResult.Win));
    player2.LoseGame(gameFactory.CreateGame(GameType.DoublePoints, player1.UserName, GameResult.Loss));

    player2.WinGame(gameFactory.CreateGame(GameType.Training, player1.UserName, GameResult.Win));
    player1.LoseGame(gameFactory.CreateGame(GameType.Training, player2.UserName, GameResult.Loss));

    player1.WinGame(gameFactory.CreateGame(GameType.DoublePoints, player2.UserName, GameResult.Win));
    player2.LoseGame(gameFactory.CreateGame(GameType.DoublePoints, player1.UserName, GameResult.Loss));

    Console.WriteLine($"\nStats of {player1.UserName} in the {player1.GetAccountType()}:");

    Console.WriteLine(player1.GetStats());

    Console.WriteLine($"Stats of {player2.UserName} in the {player2.GetAccountType()}:");
    Console.WriteLine(player2.GetStats());
  }
}