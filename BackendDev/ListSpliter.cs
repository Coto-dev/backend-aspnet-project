namespace BackendDev
{
    public static class Spliter
        { 
        public static  List<List<T>> Split<T>(this List<T> source, int count)
        {
        return source
          .Select((x, y) => new { Index = y, Value = x })
          .GroupBy(x => x.Index / count)
          .Select(x => x.Select(y => y.Value).ToList())
          .ToList();
    }
    }
}
