namespace BaseProject.Domain.Entities
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
            Active = true;
            CreationDate = DateTime.Now;
        }

        public Guid Id { get; set; }
        public bool Active { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
