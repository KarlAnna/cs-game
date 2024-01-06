using Classes.Accounts;
using Classes.Utils;

namespace Classes.Repositories
{
  public interface IGameAccountRepository
  {
    GameAccountBase CreateGameAccount(GameAccountType gameAccountType, string userName, decimal currentRating);

    GameAccountBase GameAccountId(decimal accountId);

    IEnumerable<GameAccountBase> ReadAllGameAccounts();
  }

  public class GameAccountRepository : IGameAccountRepository
  {
    private readonly DbContext _dbContext;
    private readonly GameAccountFactory _gameAccountFactory = new GameAccountFactory();

    public GameAccountRepository(DbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public GameAccountBase CreateGameAccount(GameAccountType gameAccountType, string userName, decimal currentRating)
    {
      var gameAccountId = _dbContext.GameAccounts.Count + 1;
      var gameAccount = _gameAccountFactory.CreateGameAccount(gameAccountId, gameAccountType, userName, currentRating);
      _dbContext.GameAccounts.Add(gameAccount);

      return gameAccount;
    }

    public GameAccountBase GameAccountId(decimal accountId)
    {
      return _dbContext.GameAccounts.FirstOrDefault(p => p.AccountId == accountId) ??
             throw new InvalidOperationException(ExceptionMessages.NotFoundGameAccountMsg);
    }

    public IEnumerable<GameAccountBase> ReadAllGameAccounts()
    {
      return _dbContext.GameAccounts;
    }
  }
}