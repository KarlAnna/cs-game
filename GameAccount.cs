public class GameAccount
{
    public string UserName { get; private set; }
    public decimal CurrentRating { get; private set; }
    public decimal GamesCount { get; private set; }
    private List<GameResult> gameHistory;

    public GameAccount(string userName, decimal initialRating = 1)
    {
        if (initialRating < 1)
        {
            throw new ArgumentException("Initial rating must be at least 1.");
        }
        UserName = userName;
        CurrentRating = initialRating;
        GamesCount = 0;
        gameHistory = new List<GameResult>();
    }

    public void WinGame(string opponentName, decimal rating)
    {
        if (rating < 0)
        {
            throw new ArgumentException("Rating for the game cannot be negative.");
        }
        GamesCount++;
        CurrentRating += rating;
        gameHistory.Add(new GameResult(opponentName, true, rating));
    }

    public void LoseGame(string opponentName, decimal rating)
    {
        if (rating < 0)
        {
            throw new ArgumentException("Rating for the game cannot be negative.");
        }

        GamesCount++;
        CurrentRating -= rating;
        if (CurrentRating < 1)
        {
            CurrentRating = 1;
        }
        gameHistory.Add(new GameResult(opponentName, false, rating));
    }

    public void GetStats()
    {
        Console.WriteLine($"Game statistics for user {UserName}:");
        for (int i = 0; i < gameHistory.Count; i++)
        {
            var result = gameHistory[i];
            var outcome = result.Won ? "win" : "loss";
            Console.WriteLine($"Game {i + 1}: Against {result.OpponentName}, Outcome: {outcome}, Rating: {result.Rating}");
        }
    }

    private class GameResult
    {
        public string OpponentName { get; }
        public bool Won { get; }
        public decimal Rating { get; }

        public GameResult(string opponentName, bool won, decimal rating)
        {
            OpponentName = opponentName;
            Won = won;
            Rating = rating;
        }
    }
}
