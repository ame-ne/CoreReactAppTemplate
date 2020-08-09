using CoreReactApp.Domain.Models;

namespace CoreReactApp.Auth
{
    public class UserResponse
    {
        public string Login { get; set; }
        public RoleEnum Role { get; set; }
        public string Token { get; set; }
    }
}
