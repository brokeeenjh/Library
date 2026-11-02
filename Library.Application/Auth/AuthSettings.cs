namespace Library.Application.Auth;

public class AuthSettings
{
    public string Secret { get; set; }
    public TimeSpan Expire { get; set; }
}