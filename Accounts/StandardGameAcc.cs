using Classes.Games;

namespace Classes.Accounts
{
  public class StandardGameAccount : GameAccountBase
  {
    public StandardGameAccount(decimal accountId, string userName, decimal currentRating) : base(accountId, userName, currentRating)
    {
    }

    public override string GetAccountType()
    {
      return "Standard account";
    }

    public override void WinGame(GameBase game)
    {
      base.WinGame(game);
      CurrentRating += game.CalculateRating();
    }

    public override void LoseGame(GameBase game)
    {
      base.LoseGame(game);
      CurrentRating = this.ValidateRatingAfterLoss(game.CalculateRating());
    }
  }
}