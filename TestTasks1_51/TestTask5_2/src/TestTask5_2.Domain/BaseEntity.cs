namespace TestTask5_2.Domain;

public class BaseEntity
{
    public enum EntityState : byte
    {
        Unspecified = 0,
        Created = 1,
        Updated = 2,
        Deleted = 3,
    }

    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }

    public EntityState State { get; set; } = EntityState.Unspecified;

    public void SetToDelete()
    {
        State = EntityState.Deleted;
    }

    public void SetUpdated()
    {
        if (State == EntityState.Created)
        {
            return;
        }

        ModifiedDate = DateTime.UtcNow;

        if (State != EntityState.Updated)
        {
            State = EntityState.Updated;
        }
    }

    public void ClearState()
    {
        State = EntityState.Unspecified;
    }
}