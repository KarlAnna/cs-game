namespace Classes
{
    public enum GameResult
    {
        Win, Loss
    }

    public enum GameType
    {
        Standard, Training, DoublePoints
    }

    public static class Constants
    {
        public const int MinRating = 1;
        public const string NegRatingMsg = "The rating can't be negative";
        public const string IncorrectGameResultMsg = "The transferred game result does not match the called method";
        public const decimal StandardGameRating = 100;
    }

    public abstract class GameBase
    {
        public string OpponentName { get; }
        public GameResult Result { get; }
        public GameBase(string opponentName, GameResult result)
        {
            OpponentName = opponentName;
            Result = result;
        }

        public abstract decimal CalculateRating();
    }

    public class StandardGame : GameBase
    {

        public StandardGame(string opponentName, GameResult result) : base(opponentName, result)
        {
        }

        public override decimal CalculateRating()
        {
            return Constants.StandardGameRating;
        }
    }

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

    public class DoublePointsGame : GameBase
    {

        public DoublePointsGame(string opponentName, GameResult result) : base(opponentName, result)
        {
        }

        public override decimal CalculateRating()
        {
            return Constants.StandardGameRating * 2;
        }
    }

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

    public abstract class GameAccountBase
    {
        public string UserName { get; }
        public decimal CurrentRating { get; set; }

        public List<GameBase> Games { get; } = new List<GameBase>();

        protected GameAccountBase(string userName, decimal currentRating)
        {
            UserName = userName;
            CurrentRating = currentRating;
        }

        public abstract string GetAccountType();

        private void ValidatePositiveRating(decimal rating)
        {
            if (rating < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rating), $"{Constants.NegRatingMsg}");
            }
        }

        private void VaidateGameResult(GameResult gameResult, GameResult correctGameResult)
        {
            if (gameResult != correctGameResult)
            {
                throw new ArgumentException(nameof(gameResult), $"{Constants.IncorrectGameResultMsg}");
            }
        }
        public decimal ValidateRatingAfterLoss(decimal lostRating)
        {
            var currentgRatingAfterLoss = this.CurrentRating - lostRating;
            if (currentgRatingAfterLoss <= Constants.MinRating)
            {
                return Constants.MinRating;
            }
            else
            {
                return currentgRatingAfterLoss;
            }
        }

        public virtual void WinGame(GameBase game)
        {
            var rating = game.CalculateRating();

            this.ValidatePositiveRating(rating);
            this.VaidateGameResult(game.Result, GameResult.Win);

            Games.Add(game);
        }

        public virtual void LoseGame(GameBase game)
        {
            var rating = game.CalculateRating();

            this.ValidatePositiveRating(rating);
            this.VaidateGameResult(game.Result, GameResult.Loss);

            Games.Add(game);
        }

        public string GetStats()
        {
            var history = new System.Text.StringBuilder();

            history.AppendLine($"Total current rating: {this.CurrentRating}\n");
            history.AppendLine("Opponent\tResult\tRate\tIndex");

            for (int i = 0; i < Games.Count; i++)
                history.AppendLine($"{Games[i].OpponentName}\t{Games[i].Result}\t{Games[i].CalculateRating()}\t{i}");

            return history.ToString();
        }
    }

    public class StandardGameAccount : GameAccountBase
    {
        public StandardGameAccount(string userName, decimal currentRating) : base(userName, currentRating)
        {
        }

        public override string GetAccountType()
        {
            return "Standard account";
        }

        public override void WinGame(GameBase game)
        {
            base.WinGame(game);
            this.CurrentRating += game.CalculateRating();
        }

        public override void LoseGame(GameBase game)
        {
            base.LoseGame(game);
            this.CurrentRating = this.ValidateRatingAfterLoss(game.CalculateRating());
        }
    }

    public class DoublePointsForLossAccount : GameAccountBase
    {
        public DoublePointsForLossAccount(string userName, decimal currentRating) : base(userName, currentRating)
        {
        }

        public override string GetAccountType()
        {
            return "Double points account";
        }

        public override void WinGame(GameBase game)
        {
            base.WinGame(game);
            this.CurrentRating += game.CalculateRating();
        }

        public override void LoseGame(GameBase game)
        {
            base.LoseGame(game);
            this.CurrentRating = this.ValidateRatingAfterLoss(game.CalculateRating() * 2);
        }
    }

    public class WinStreakGameAccount : GameAccountBase
    {
        private int consecutiveWins;

        public WinStreakGameAccount(string userName, decimal currentRating) : base(userName, currentRating)
        {
        }

        public override string GetAccountType()
        {
            return "Win streak account";
        }


        public override void WinGame(GameBase game)
        {
            base.WinGame(game);
            this.consecutiveWins += 1;
            this.CurrentRating += game.CalculateRating() * this.consecutiveWins;
        }

        public override void LoseGame(GameBase game)
        {
            base.LoseGame(game);

            this.consecutiveWins = 0;
            this.CurrentRating = this.ValidateRatingAfterLoss(game.CalculateRating());
        }
    }
}