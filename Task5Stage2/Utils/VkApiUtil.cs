using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
using Task5Stage2.Models;
using Task5Stage2.RestApi;

namespace Task5Stage2.Utils;

public static class VkApiUtil
{
   
    private static ISettingsFile config = new JsonSettingsFile(Environment.CurrentDirectory + @"\Resources\config.json");
    private static string apiVersion = config.GetValue<string>("api.version");
    private static string baseUrl = config.GetValue<string>("api.baseUrl");
    private static string wallPostMethod = config.GetValue<string>("api.methods.wallPost");
    private static string wallCreateCommentMethod = config.GetValue<string>("api.methods.wallCreateComment");
    private static string wallEditPostMethod = config.GetValue<string>("api.methods.wallEditPost");
    private static string wallGetLikesToThePostMethod = config.GetValue<string>("api.methods.wallGetLikesToThePost");
    private static string wallDeletePostMethod = config.GetValue<string>("api.methods.wallDeletePost");
    private static string photosGetWallUploadServerMethod = config.GetValue<string>("api.methods.photosGetWallUploadServer");
    private static string photosSaveWallPhotoMethod = config.GetValue<string>("api.methods.photosSaveWallPhoto");
    
    public static int WallPost(string postMessage, string token)
    {
    
        var parameters = new Dictionary<string, string>
        {
            { "message", postMessage },
            { "access_token", token },
            { "v", apiVersion },
        };

        var paramsContent = new FormUrlEncodedContent(parameters);

        var request = new RestRequest(baseUrl, wallPostMethod);
        request.AddContent(paramsContent);
        var response = RestClient.Post(request);
        var resultResponse = response.Deserialize<WallPostResult>();
        return resultResponse.response.post_id;
    }

    public static void WallPostComment(string postId, string commentMessage,
        string token)
    {
        var parameters = new Dictionary<string, string>
        {
            { "post_id", postId },
            { "message", commentMessage },
            { "access_token", token },
            { "v", apiVersion },
        };

        var paramsContent = new FormUrlEncodedContent(parameters);

        var request = new RestRequest(baseUrl, wallCreateCommentMethod);
        request.AddContent(paramsContent);
        RestClient.Post(request);
    }

    public static void EditWallPostAddImage(string postId, string postMessage, string imgPath, string token)
    {
        var image = LoadImage(imgPath, token);

        var parameters = new Dictionary<string, string>
        {
            { "post_id", postId },
            { "message", postMessage },
            { "access_token", token },
            { "v", apiVersion },
            { "attachments", $"photo{image[0].owner_id}_{image[0].id}" }
        };

        var paramsContent = new FormUrlEncodedContent(parameters);

        var request = new RestRequest(baseUrl, wallEditPostMethod);
        request.AddContent(paramsContent);
        RestClient.Post(request);
    }

    public static List<GetLikesResult.User> AddLikeToThePost(string postId, string token)
    {
        var parameters = new Dictionary<string, string>
        {
            { "post_id", postId },
            { "access_token", token },
            { "v", apiVersion },
        };

        var paramsContent = new FormUrlEncodedContent(parameters);

        var request = new RestRequest(baseUrl, wallGetLikesToThePostMethod);
        request.AddContent(paramsContent);
        var response = RestClient.Post(request);
        var resultResponse = response.Deserialize<GetLikesResult>();
        return resultResponse.response.users;
    }

    public static void DeleteWallPost(string postId, string token)
    {
        var parameters = new Dictionary<string, string>
        {
            { "post_id", postId },
            { "access_token", token },
            { "v", apiVersion },
        };

        var paramsContent = new FormUrlEncodedContent(parameters);

        var request = new RestRequest(baseUrl, wallDeletePostMethod);
        request.AddContent(paramsContent);
        RestClient.Post(request);
    }

    private static List<SavePhotoResult.Response> LoadImage(string imgPath, string token)
    {
        var parametersUploadServer = new Dictionary<string, string>
        {
            { "access_token", token },
            { "v", apiVersion }
        };

        var paramsUploadServerContent = new FormUrlEncodedContent(parametersUploadServer);

        var uploadServerRequest = new RestRequest(baseUrl, photosGetWallUploadServerMethod);
        uploadServerRequest.AddContent(paramsUploadServerContent);
        var uploadServerResponse = RestClient.Post(uploadServerRequest);
        var resultUploadServerResponse = uploadServerResponse.Deserialize<GetWallUploadServerResult>();
        var uploadUrl = resultUploadServerResponse.response.upload_url;

        byte[] imgdata = File.ReadAllBytes(imgPath);
        var imageContent = new ByteArrayContent(imgdata);
        var multipartContent = new MultipartFormDataContent();
        multipartContent.Add(imageContent, "photo", "image.jpg");
        var loadPhotoRequest = new RestRequest(uploadUrl, "");
        loadPhotoRequest.AddContent(multipartContent);
        var loadPhotoResponse = RestClient.Post(loadPhotoRequest);
        var resultLoadPhotoResponse = loadPhotoResponse.Deserialize<LoadPhotoResult>(RestResponse.EncodingType.win1251);

        var parametersSavePhoto = new Dictionary<string, string>
        {
            { "photo", resultLoadPhotoResponse.photo },
            { "server", resultLoadPhotoResponse.server },
            { "hash", resultLoadPhotoResponse.hash },
            { "access_token", token },
            { "v", apiVersion }
        };

        var paramsSavePhotoContent = new FormUrlEncodedContent(parametersSavePhoto);

        var savePhotoRequest = new RestRequest(baseUrl, photosSaveWallPhotoMethod);
        savePhotoRequest.AddContent(paramsSavePhotoContent);
        var savePhotoResponse = RestClient.Post(savePhotoRequest);
        var resultSavePhotoResponse = savePhotoResponse.Deserialize<SavePhotoResult>();
        return resultSavePhotoResponse.response;
    }
}