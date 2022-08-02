using Newtonsoft.Json.Linq;

namespace Task4Stage2.RestApiFramework.Utils;

public class JsonSettingsFileUtil
{
    private JObject jSettings;
    
    public JsonSettingsFileUtil(string subPath)
    {
        var jsonFile = File.ReadAllText(Environment.CurrentDirectory + subPath);
        jSettings = JObject.Parse(jsonFile);
    }

    public T GetValue<T>(string key)
    {
        return jSettings[key].ToObject<T>();
    }
}