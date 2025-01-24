namespace PublicRecords.Domain.Extensions
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            if (collection == null) return true;

            return collection.Count == 0;
        }
    }
}
