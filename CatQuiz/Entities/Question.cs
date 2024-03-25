using CatQuiz.Data;
using CatQuiz.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatQuiz.Entities;

public class Question : BaseEntity
{
    public required string ExternalImageId { get; set; }

    public required string ImageUrl { get; set; }

    public AnswerStatus AnswerStatus { get; set; }

    public required string CorrectBreedId { get; set; }

    public Guid UserId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }
}
