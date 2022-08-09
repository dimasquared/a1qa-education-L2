using Task5Stage2.Models;
using Task5Stage2.RestApi;

namespace Task5Stage2;

public static class VkApiUtil
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

    public static void WallPostComment(string postId, string commentMessage, string token, string apiVersion)
    {
        var parameters = new Dictionary<string, string>
        {
            {"post_id", postId},
            { "message", commentMessage },
            { "access_token", token },
            { "v", apiVersion },
        };

        var paramsContent = new FormUrlEncodedContent(parameters);
        
        var request = new RestRequest("https://api.vk.com/method/", "wall.createComment");
        request.AddContent(paramsContent);
        RestClient.Post(request);
    }
    
    public static void DeleteWallPost(string postId, string token, string apiVersion)
    {
        var parameters = new Dictionary<string, string>
        {
            {"post_id", postId},
            { "access_token", token },
            { "v", apiVersion },
        };

        var paramsContent = new FormUrlEncodedContent(parameters);
        
        var request = new RestRequest("https://api.vk.com/method/", "wall.delete");
        request.AddContent(paramsContent);
        RestClient.Post(request);
    }
}