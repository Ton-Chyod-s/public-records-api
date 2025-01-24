using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PublicRecords.Domain.Enums.OfficialStateDiaries;

namespace PublicRecords.Domain.Entities.OfficialStateDiary
{
    public class OfficialDiaries : BaseEntity.BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Number { get; private set; }
        public string Day { get; private set; }
        public string File { get; private set; }
        public string Description { get; private set; }
        public long? SessionId { get; private set; } 
        public long PersonId { get; private set; }
        public TypeDiaryEnum Type { get; private set; }

        private OfficialDiaries() { }
      
        public OfficialDiaries(string number, string day, string file, string description, long? sessionId, long personId, TypeDiaryEnum type)
        {
            Number = number;
            Day = day;
            File = file;
            Description = description;
            SessionId = sessionId;
            PersonId = personId;
            Type = type;
        }

        #region [Foreign Key]
        [ForeignKey(nameof(SessionId))]
        public Session.Session Session { get; private set; }

        [ForeignKey(nameof(PersonId))]
        public Person.Person Person { get; private set; }
        #endregion
    }
}
