using Task5Stage2.Models;
using Task5Stage2.RestApi;

namespace Task5Stage2;

public class VkApi
{
    public static int WallPost(string postMessage, string token, string apiVersion)
    {
        var parameters = new Dictionary<string, string>
        {
            { "message", postMessage },
            { "access_token", token },
            { "v", apiVersion },
        };

        var paramsContent = new FormUrlEncodedContent(parameters);

        var request = new RestRequest("https://api.vk.com/method/", "wall.post");
        request.AddContent(paramsContent);
        var response = RestClient.Post(request);
        var resultResponse = response.Deserialize<WallPostResult>();
        return resultResponse.response.post_id;
    }
}