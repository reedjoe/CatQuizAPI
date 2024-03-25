using CatQuiz.Entities;
using CatQuiz.Shared.Enums;

namespace CatQuiz.Data;

/// <summary>
/// Seeds the DB with some dummy data for testing purposes
/// </summary>
public class DataSeed
{
    public async Task SeedData(DataContext context)
    {
        SeedUsersAndQuestions(context);
        await context.SaveChangesAsync();
    }

    private void SeedUsersAndQuestions(DataContext context)
    {
        var johnUserId = Guid.NewGuid();
        var janeUserId = Guid.NewGuid();
        var joeUserId = Guid.NewGuid();

        var users = new List<User>()
        {
            new User()
            {
                Id = johnUserId,
                Email = "johnsmith@test.com",
                FirstName = "John",
                LastName = "Smith"
            },
            new User()
            {
                Id = janeUserId,
                Email = "janedoe@test.com",
                FirstName = "Jane",
                LastName = "Doe"
            },
            new User()
            {
                Id = joeUserId,
                Email = "joebloggs@test.com",
                FirstName = "Joe",
                LastName = "Bloggs"
            }
        };

        context.Users.AddRange(users);

        var questions = new List<Question>()
        {
            new Question()
            {
                Id = Guid.NewGuid(),
                ExternalImageId = "0XYvRd7oD",
                ImageUrl = "https://cdn2.thecatapi.com/images/0XYvRd7oD.jpg",
                AnswerStatus = AnswerStatus.Incorrect,
                CorrectBreedId = "abys",
                UserId = johnUserId
            },
            new Question()
            {
                Id = Guid.NewGuid(),
                ExternalImageId = "0XYvRd7oD",
                ImageUrl = "https://cdn2.thecatapi.com/images/0XYvRd7oD.jpg",
                AnswerStatus = AnswerStatus.Correct,
                CorrectBreedId = "abys",
                UserId = johnUserId
            },
            new Question()
            {
                Id = Guid.NewGuid(),
                ExternalImageId = "0XYvRd7oD",
                ImageUrl = "https://cdn2.thecatapi.com/images/0XYvRd7oD.jpg",
                AnswerStatus = AnswerStatus.Correct,
                CorrectBreedId = "abys",
                UserId = janeUserId
            },
            new Question()
            {
                Id = Guid.NewGuid(),
                ExternalImageId = "0XYvRd7oD",
                ImageUrl = "https://cdn2.thecatapi.com/images/0XYvRd7oD.jpg",
                AnswerStatus = AnswerStatus.Correct,
                CorrectBreedId = "abys",
                UserId = janeUserId
            },
            new Question()
            {
                Id = Guid.NewGuid(),
                ExternalImageId = "0XYvRd7oD",
                ImageUrl = "https://cdn2.thecatapi.com/images/0XYvRd7oD.jpg",
                AnswerStatus = AnswerStatus.Correct,
                CorrectBreedId = "abys",
                UserId = janeUserId
            },
            new Question()
            {
                Id = Guid.NewGuid(),
                ExternalImageId = "0XYvRd7oD",
                ImageUrl = "https://cdn2.thecatapi.com/images/0XYvRd7oD.jpg",
                AnswerStatus = AnswerStatus.Correct,
                CorrectBreedId = "abys",
                UserId = janeUserId
            },
            new Question()
            {
                Id = Guid.NewGuid(),
                ExternalImageId = "0XYvRd7oD",
                ImageUrl = "https://cdn2.thecatapi.com/images/0XYvRd7oD.jpg",
                AnswerStatus = AnswerStatus.Incorrect,
                CorrectBreedId = "abys",
                UserId = janeUserId
            }
        };

        context.Questions.AddRange(questions);
    }
}
