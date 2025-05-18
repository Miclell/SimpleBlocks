using Newtonsoft.Json.Linq;

namespace SimpleBlocks.Client.Extensions;

public static class JObjectExtensions
{
    public static Dictionary<string, string> ToDictionary(this JObject jObject, string parentKey = "")
    {
        var result = new Dictionary<string, string>();

        foreach (var property in jObject.Properties())
        {
            var key = string.IsNullOrEmpty(parentKey) ? property.Name : $"{parentKey}.{property.Name}";
            if (property.Value is JObject nestedObject)
            {
                var nestedItems = nestedObject.ToDictionary(key);
                foreach (var item in nestedItems)
                {
                    result.Add(item.Key, item.Value);
                }
            }
            else
                result.Add(key, property.Value.ToString());
        }

        return result;
    }
}
