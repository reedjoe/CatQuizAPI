namespace CatQuiz.Core.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count = 1)
    {
        if (source.Count() < count)
        {
            return Enumerable.Empty<T>();
        }

        return source.RandomiseOrder().Take(count);
    }

    public static IEnumerable<T> RandomiseOrder<T>(this IEnumerable<T> source)
    {
        return source.OrderBy(x => Guid.NewGuid());
    }
}
