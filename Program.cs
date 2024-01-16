using Classes.Utils;
using Classes.Repositories;
using Classes.Services;
using Classes.UI;

namespace Classes
{
  class Program
  {

    static void Main()
    {
      Run();
    }

    static void AddMockData(GameAccountService gameAccService, GameService gameService)
    {
      var player1 = gameAccService.CreateAccount(GameAccountType.Standard, "Player 1", GameConstants.MinRating);
      var player2 = gameAccService.CreateAccount(GameAccountType.WinStreak, "Player 2", GameConstants.MinRating);

      player1.WinGame(gameService.CreateGame(GameType.Standard, player2.UserName, GameResult.Win));
      player2.LoseGame(gameService.CreateGame(GameType.Standard, player1.UserName, GameResult.Loss));

      player2.WinGame(gameService.CreateGame(GameType.Training, GameConstants.TrainingGameOpponentName, GameResult.Win));
    }

    static void Run()
    {
      var dbContext = new DbContext();
      var gameAccountRepository = new GameAccountRepository(dbContext);
      var gameRepository = new GameRepository(dbContext);

      var gameService = new GameService(gameRepository);
      var gameAccService = new GameAccountService(gameAccountRepository);

      var GameUI = new GameUI(gameService, gameAccService);
      var GameAccUI = new GameAccountsUI(gameAccService);


      AddMockData(gameAccService, gameService);

      Console.WriteLine("\n\nWelcome!");

      Dictionary<int, (string commandInfo, Action command)> uiCommands = new Dictionary<int, (string, Action)>
      {
        { 1, ("Create a game account", GameAccUI.CreateGameAccount) },
        { 2, ("Let's play!", GameUI.CreateGame) },
        { 3, ("Print the list of players", GameAccUI.ShowAllGameAccounts) },
        { 4, ("Print the list of games", GameUI.ShowAllGames) },
        { 5, ("Exit", () => Environment.Exit(0)) }
      };

      while (true)
      {
        Console.WriteLine("\n-----------------");

        foreach (var (optionToPrint, (commandInfo, _)) in uiCommands)
          Console.WriteLine($"{optionToPrint}. {commandInfo}");

        Console.Write("\nChoose an option: ");

        var choice = Console.ReadLine();

        if (int.TryParse(choice, out var optionToChoose) && uiCommands.ContainsKey(optionToChoose))
        {
          uiCommands[optionToChoose].command();
        }
        else
        {
          Console.WriteLine("\nThis option does not exist. Try another one.");
        }
      }
    }
  }
}