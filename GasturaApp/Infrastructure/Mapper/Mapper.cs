using System.Reflection;

namespace GasturaApp.Infrastructure.Mapper;

public class Mapper
{
    static Dictionary<(Type from, Type to), List<(MethodInfo Get, MethodInfo Set)>> _cache = [];

    public static T Map<T>(object f) where T : class, new()
    {
        var key = (from: f.GetType(), to: typeof(T));

        if (!_cache.ContainsKey(key))
        {
            PopulateCacheKey(key);
        }

        var result = new T();
        var entry = _cache[key];
        foreach (var (Get, Set) in entry)
        {
            var value = Get.Invoke(f, null);
            Set.Invoke(result, [value]);
        }

        return result;
    }

    public static void PopulateCacheKey((Type from, Type to) key)
    {
        var fromProps = key.from.GetProperties();
        var toProps = key.to.GetProperties();

        List<(MethodInfo, MethodInfo)> entry = [];

        foreach (var from in fromProps)
        {
            var to = toProps.FirstOrDefault(x => x.Name == from.Name);
            if (to == null)
                continue;

            if (from.GetMethod != null && to.SetMethod != null)
                entry.Add((from.GetMethod, to.SetMethod));
        }

        _cache[key] = entry;
    }
}
