using Aquality.Selenium.Browsers;
using Task5Stage2.Models;
using Task5Stage2.RestApi;

namespace Task5Stage2.Utils;

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
    
    public static void EditWallPost(string postId, string postMessage, string token, string apiVersion)
    {
        var parameters = new Dictionary<string, string>
        {
            {"post_id", postId},
            { "message", postMessage },
            { "access_token", token },
            { "v", apiVersion },
        };

        var paramsContent = new FormUrlEncodedContent(parameters);

        var request = new RestRequest("https://api.vk.com/method/", "wall.edit");
        request.AddContent(paramsContent);
        RestClient.Post(request);
        AqualityServices.ConditionalWait.WaitFor(() => false, TimeSpan.FromSeconds(0.5));
    }
    
    public static List<GetLikesResult.User> AddLikeToThePost(string postId, string token, string apiVersion)
    {
        var parameters = new Dictionary<string, string>
        {
            { "post_id", postId },
            { "access_token", token },
            { "v", apiVersion },
        };

        var paramsContent = new FormUrlEncodedContent(parameters);

        var request = new RestRequest("https://api.vk.com/method/", "wall.getLikes");
        request.AddContent(paramsContent);
        var response = RestClient.Post(request);
        var resultResponse = response.Deserialize<GetLikesResult>();
        return resultResponse.response.users;
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