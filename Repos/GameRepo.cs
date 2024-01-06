using Classes.Utils;
using Classes.Games;

namespace Classes.Repositories
{
  public interface IGameRepository
  {
    GameBase CreateGame(GameType gameType, string opponentName, GameResult result);

    IEnumerable<GameBase> ReadAllGames();

    IEnumerable<GameBase> ReadGamesByGameAccountId(decimal accountId);
  }

  public class GameRepository : IGameRepository
  {
    private readonly GameFactory _gameFactory = new GameFactory();
    private readonly DbContext _dbContext;

    public GameRepository(DbContext dbContext)
    {
      this._dbContext = dbContext;
    }

    public GameBase CreateGame(GameType gameType, string opponentName, GameResult result)
    {
      var game = _gameFactory.CreateGame(gameType, opponentName, result);
      _dbContext.Games.Add(game);

      return game;
    }

    public IEnumerable<GameBase> ReadAllGames()
    {
      return _dbContext.Games;
    }

    public IEnumerable<GameBase> ReadGamesByGameAccountId(decimal accountId)
    {
      return _dbContext.Games.Where(g =>
        g.OpponentName == _dbContext.GameAccounts.FirstOrDefault(p => p.AccountId == accountId)?.UserName);
    }
  }
}