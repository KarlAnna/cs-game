using Classes.Utils;
using Classes.Games;
using Classes.Repositories;

namespace Classes.Services
{
  public interface IGameService
  {
    GameBase CreateGame(GameType gameType, string opponentName, GameResult result);

    IEnumerable<GameBase> GetAllGames();

    IEnumerable<GameBase> GetGamesByGameAccountId(decimal accountId);
  }

  public class GameService : IGameService
  {
    private readonly IGameRepository _gameRepository;

    public GameService(IGameRepository gameRepository)
    {
      _gameRepository = gameRepository;
    }

    public GameBase CreateGame(GameType gameType, string opponentName, GameResult result)
    {
      Console.WriteLine(
        $"\n{Symbols.CheckMark} The {gameType} game has been played with the {result} result against {opponentName} ");

      return _gameRepository.CreateGame(gameType, opponentName, result);
    }

    public IEnumerable<GameBase> GetAllGames()
    {
      return _gameRepository.ReadAllGames();
    }

    public IEnumerable<GameBase> GetGamesByGameAccountId(decimal accountId)
    {
      return _gameRepository.ReadGamesByGameAccountId(accountId);
    }
  }
}