namespace IceCreamApi.Domain;

public class Account
{
  public Guid? Id { get; set; }
  public string UserName { get; set; }
  public UserType UserType { get; set; }

  public Account(string userName, UserType userType)
  {
    UserName = userName;
    UserType = userType;
  }
}