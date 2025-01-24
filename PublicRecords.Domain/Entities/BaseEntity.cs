namespace PublicRecords.Domain.Entities.BaseEntity
{
    public abstract class BaseEntity
    {
        public long Id { get; init; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        protected BaseEntity()
        {
        }
    }
}
