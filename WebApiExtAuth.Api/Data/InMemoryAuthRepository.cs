using System.Collections.Concurrent;
using System.Collections.Generic;

namespace WebApiExtAuth.Api.Data
{
  public class InMemoryAuthRepository
  {
    public static readonly ConcurrentDictionary<string, User> Users = new ConcurrentDictionary<string, User>(new Dictionary<string, User>() {
      {"ciccio", new User() {
        UserName = "ciccio",
        Password = "ciccio"
      }}
    });

    public User Add(string userName, string password)
    {
      var user = new User() {
        UserName = userName,
        Password = password
      };
      return Users.TryAdd(userName, user) ? user : null;
    }

    public User Find(string userName, string password)
    {
      User user ;
      var found = Users.TryGetValue(userName, out user);
      return found && password == user.Password ? user : default(User);
    }
  }
}