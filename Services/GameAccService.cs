using Classes.Accounts;
using Classes.Utils;
using Classes.Repositories;

namespace Classes.Services
{
  public interface IGameAccountService
  {
    GameAccountBase CreateAccount(GameAccountType gameAccountType, string userName, decimal currentRating);

    GameAccountBase GetAccountById(decimal accountId);

    IEnumerable<GameAccountBase> GetAllAccounts();
  }

  public class GameAccountService : IGameAccountService
  {
    private readonly IGameAccountRepository _gameAccountRepository;
    public GameAccountService(IGameAccountRepository gameAccountRepository)
    {
      _gameAccountRepository = gameAccountRepository;
    }

    public GameAccountBase CreateAccount(GameAccountType gameAccountType, string userName, decimal currentRating)
    {
      return _gameAccountRepository.CreateGameAccount(gameAccountType, userName, currentRating);
    }

    public GameAccountBase GetAccountById(decimal accountId)
    {
      return _gameAccountRepository.GameAccountId(accountId);
    }

    public IEnumerable<GameAccountBase> GetAllAccounts()
    {
      return _gameAccountRepository.ReadAllGameAccounts();
    }
  }
}