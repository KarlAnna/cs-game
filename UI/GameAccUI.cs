using Classes.Utils;
using Classes.Services;

namespace Classes.UI
{

  public class GameAccountsUI
  {
    private readonly GameAccountService _gameAccountService;


    public GameAccountsUI(GameAccountService gameAccountService)
    {
      _gameAccountService = gameAccountService;
    }


    public void ShowAllGameAccounts()
    {
      var allGameAccounts = _gameAccountService.GetAllAccounts();
      if (!allGameAccounts.Any())
      {
        Console.WriteLine("\nYou have not created any games accounts yet");
      }
      else
      {
        Console.WriteLine("List of all accounts:");
        foreach (var gameAccount in _gameAccountService.GetAllAccounts())
          Console.WriteLine(
            $"{gameAccount.AccountId}: {gameAccount.UserName}, Rating: {gameAccount.CurrentRating}");
      }
    }

    public void CreateGameAccount()
    {
      Console.Write("\nEnter the player's name: ");
      var name = Console.ReadLine();

      if (string.IsNullOrWhiteSpace(name))
      {
        throw new Exception(ExceptionMessages.NullTypedPlayerName);
      }

      Console.Write(
        "\nChoose type of game account\n1. Standard\n2. Win strike (extra points are awarded for scoring wins)\n3. Double points for loss\n(if you select anything else, the standart account will automatically be selected): ");
      var accountType = Console.ReadLine();

      var mappedReceivedAccountTypeToRealOne = accountType switch
      {
        "2" => GameAccountType.WinStreak,
        "3" => GameAccountType.DoublePoints,
        _ => GameAccountType.Standard
      };

      _gameAccountService.CreateAccount(mappedReceivedAccountTypeToRealOne, name, GameConstants.MinRating);
    }
  }
}