using PublicRecords.Domain.Entities.Token;
using PublicRecords.Domain.Enums.User;

namespace PublicRecords.Domain.Entities.User
{
    public class User : BaseEntity.BaseEntity
    {
        public string UserName { get; private set; }
        public string PassWordHash { get; private set; }
        public bool? IsActive { get; private set; }
        public UserEnum? Roles { get; private set; } 
        public ICollection<AuthToken> AuthToken { get; private set; }

        private User() { }

        public User(string username, string passwordHash)
        {
            UserName = username;
            PassWordHash = passwordHash;
        }

        public void UpdateUser(string username, bool? isActive, UserEnum? roles)
        {
            UserName = username;
            IsActive = isActive;
            Roles = roles;
        }
    }
}
