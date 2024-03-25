using CatQuiz.Data;

namespace CatQuiz.Entities;

public class User : BaseEntity
{
    public required string Email { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public virtual ICollection<Question> Questions { get; set; }
}
