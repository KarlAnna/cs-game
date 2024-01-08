using Classes.Utils;

namespace Classes.Games
{
  public class TrainingGame : GameBase
  {
    public TrainingGame(string opponentName, GameResult result) : base(opponentName, result)
    {
    }

    public override decimal CalculateRating()
    {
      return 0;
    }
  }
}