namespace CoreReactApp.Auth
{
    public interface IAuthService
    {
        UserResponse Authenticate(string login, string password);
    }
}
