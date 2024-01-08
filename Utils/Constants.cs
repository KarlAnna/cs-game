namespace Classes.Utils
{
  public static class GameConstants
  {
    public const int MinRating = 1;
    public const decimal StandardGameRating = 100;
    public const string TrainingGameOpponentName = "Training Bot";
  }

  public static class ExceptionMessages
  {
    public const string NegRatingMsg = "The rating can't be negative";
    public const string IncorrectGameResultMsg = "The transferred game result does not match the called method";
    public const string NotFoundGameAccountMsg = "Such a game account does not exist";
    public const string NullTypedPlayerName = "The player's typed name must not be null or whitespace.";
  }

  public static class Symbols
  {
    public const string CheckMark = "\u2611";
  }
}