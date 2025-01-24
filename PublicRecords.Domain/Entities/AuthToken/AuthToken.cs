using System.ComponentModel.DataAnnotations.Schema;

namespace PublicRecords.Domain.Entities.Token
{
    public class AuthToken : BaseEntity.BaseEntity
    {
        public string Bearer { get; private set; }
        public long UserId { get; private set; }

        private AuthToken() { } 

        public AuthToken(string bearer, long userId)
        {
            Bearer = bearer;
            UserId = userId;
        }

        public void UpdateBearer(string bearer)
        {
            Bearer = bearer;
        }

        #region [Foreign Key]
        [ForeignKey(nameof(UserId))]
        public User.User User { get; set; }
        #endregion
    }
}
