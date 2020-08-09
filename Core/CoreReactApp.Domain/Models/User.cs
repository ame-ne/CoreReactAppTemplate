using System.Text.Json.Serialization;

namespace CoreReactApp.Domain.Models
{
    public class User : BaseEntity
    {
        public string Login { get; set; }
        public RoleEnum Role { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
}
