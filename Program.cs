using Classes.Utils;
using Classes.Repositories;
using Classes.Services;

namespace Classes
{
  class Program
  {
    static void Main()
    {
      Run();
    }
    static void Run()
    {
      var dbContext = new DbContext();
      var gameAccRepo = new GameAccountRepository(dbContext);
      var gameRepo = new GameRepository(dbContext);

      var gameService = new GameService(gameRepo);
      var gameAccService = new GameAccountService(gameAccRepo);

      var player1 = gameAccService.CreateAccount(GameAccountType.Standard, "Player 1", GameConstants.MinRating);
      var player2 = gameAccService.CreateAccount(GameAccountType.WinStreak, "Player 2", GameConstants.MinRating);
      var player3 = gameAccService.CreateAccount(GameAccountType.DoublePoints, "Player 3", GameConstants.MinRating);
      var player4 = gameAccService.CreateAccount(GameAccountType.Standard, "Player 4", GameConstants.MinRating);

      player1.WinGame(gameService.CreateGame(GameType.Standard, player2.UserName, GameResult.Win));
      player2.LoseGame(gameService.CreateGame(GameType.Standard, player1.UserName, GameResult.Loss));

      player2.WinGame(gameService.CreateGame(GameType.Training, GameConstants.TrainingGameOpponentName, GameResult.Win));

      player1.WinGame(gameService.CreateGame(GameType.DoublePoints, player3.UserName, GameResult.Win));
      player3.LoseGame(gameService.CreateGame(GameType.DoublePoints, player1.UserName, GameResult.Loss));

      player2.WinGame(gameService.CreateGame(GameType.Standard, player4.UserName, GameResult.Win));
      player4.LoseGame(gameService.CreateGame(GameType.Standard, player2.UserName, GameResult.Loss));


      Console.WriteLine("List of all accounts:");
      foreach (var gameAccount in gameAccService.GetAllAccounts())
        Console.WriteLine($"{gameAccount.AccountId}: {gameAccount.UserName}, Rating: {gameAccount.CurrentRating}");

      Console.WriteLine("\nList of all games:");
      foreach (var game in gameService.GetAllGames())
        Console.WriteLine($"{game.OpponentName}, Result: {game.Result}, Rating: {game.CalculateRating()}");

      Console.WriteLine(
        $"\nStats of {player1.UserName} in the {player1.GetAccountType()}:");
      Console.WriteLine(gameAccService.GetAccountById(player1.AccountId).GetStats());

      Console.WriteLine(
        $"\nStats of {player2.UserName} in the {player1.GetAccountType()}:");
      Console.WriteLine(gameAccService.GetAccountById(player2.AccountId).GetStats());
    }
  }
}