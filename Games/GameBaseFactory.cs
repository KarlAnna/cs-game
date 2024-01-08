using Classes.Utils;

namespace Classes.Games
{
  public class GameFactory
  {
    public GameBase CreateGame(GameType gameType, string opponentName, GameResult result)
    {
      switch (gameType)
      {
        case GameType.Standard:
          return new StandardGame(opponentName, result);
        case GameType.Training:
          return new TrainingGame(opponentName, result);
        case GameType.DoublePoints:
          return new DoublePointsGame(opponentName, result);
        default:
          throw new ArgumentException("Invalid game type");
      }
    }
  }
}