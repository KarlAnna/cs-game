class Program
{
  static void Main()
  {
    Run();
  }

  static void Run()
  {
    GameAccount player1 = new GameAccount("Player1", 1);
    GameAccount player2 = new GameAccount("Player2", 1);

    player1.WinGame("Player2", 400);
    player2.LoseGame("Player1", 0);

    player1.WinGame("Player2", 800);
    player2.LoseGame("Player1", 0);

    player1.LoseGame("Player2", 400);
    player2.WinGame("Player1", 400);

    player1.GetStats();
    player2.GetStats();
  }
}