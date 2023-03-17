using Newtonsoft.Json;

namespace Task4Stage2.RestApiFramework.Utils;

public static class JsonUtil
{
    public static bool TryToDeserializeObject<T>(string json, out T? obj)
    {
        try
        {
            obj = JsonConvert.DeserializeObject<T>(json);
            return true;
        }
        catch (JsonSerializationException e)
        {
            obj = default(T);
            return false;
        }
    }
    
}