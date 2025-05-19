namespace ProjOb;

public static class ListExtensions
{
    public static T PopBack<T>(this List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            throw new InvalidOperationException("The list is empty.");
        }
        int ind = list.Count - 1;
        var lastElement = list[ind];
        list.RemoveAt(ind);
        return lastElement;
    }
}