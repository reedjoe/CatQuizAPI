namespace CatQuiz.Data;

public abstract class BaseEntity
{
    public Guid Id { get; set; }

    public virtual DateTime CreatedDate { get; set; }

    public virtual DateTime LastModifiedDate { get; set; }
}
