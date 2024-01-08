using Classes.Games;

namespace Classes.Accounts
{
  public class WinStreakGameAccount : GameAccountBase
  {
    private int _consecutiveWins;

    public WinStreakGameAccount(decimal accountId, string userName, decimal currentRating) : base(accountId, userName,
      currentRating)
    {
    }

    public override string GetAccountType()
    {
      return "Win streak account";
    }

    public override void WinGame(GameBase game)
    {
      base.WinGame(game);
      _consecutiveWins += 1;
      CurrentRating += game.CalculateRating() * _consecutiveWins;
    }

    public override void LoseGame(GameBase game)
    {
      base.LoseGame(game);

      _consecutiveWins = 0;
      CurrentRating = this.ValidateRatingAfterLoss(game.CalculateRating());
    }
  }


}