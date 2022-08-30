using Task6Stage2.RestApiFramework.Utils;

namespace Task6Stage2.DataBase.Utils;

public class DbConfigUtil
{
    public static string GetConnectionString()
    {
        JsonSettingsFileUtil jConfig = new JsonSettingsFileUtil(@"\Resources\config.json");
        var server = jConfig.GetValue<string>("server");
        var user = jConfig.GetValue<string>("user");
        var password = jConfig.GetValue<string>("password");
        var database = jConfig.GetValue<string>("database");
        return $"server={server};user={user};password={password};database={database};";
    }
}