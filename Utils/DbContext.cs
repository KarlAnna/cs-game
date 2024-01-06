using Classes.Accounts;
using Classes.Games;

namespace Classes.Utils
{
  public class DbContext
  {
    public List<GameAccountBase> GameAccounts { get; } = new List<GameAccountBase>();
    public List<GameBase> Games { get; } = new List<GameBase>();
  }
}