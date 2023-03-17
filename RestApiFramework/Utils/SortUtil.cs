namespace Task4Stage2.RestApiFramework.Utils;

public static class SortUtil
{
    public static bool CheckAscendingSortUtil<T>(T[] data, Func<T, int> getSortValue)
    {
        for (var i = 0; i < data.Length - 1; i++)
        {
            var currentPost = data[i];
            var nextPost = data[i + 1];
            if (getSortValue(nextPost) < getSortValue(currentPost))
            {
                return false;
            }
        }

        return true;
    }
}