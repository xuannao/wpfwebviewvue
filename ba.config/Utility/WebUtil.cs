using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ba.config.Utility;

internal static class WebUtil
{
    public static Dictionary<string, string> DaToDc(object? obj, IEnumerable<string> keys)
    {
        Dictionary<string, string> data = new();
        if (obj == null || obj.ToString() == null) return data;
        var source = JsonConvert.DeserializeObject<JObject>(obj.ToString()!);

        if (source == null || source.Count <= 0 || keys == null) return data;

        Dictionary<string, string> res = new();
        foreach (var key in keys)
        {
            if (!source.ContainsKey(key) || source[key] == null)
            {
                return data;
            }
            else
            {
                res.Add(key, source[key]!.ToString());
            }
        }
        return res;
    }
}