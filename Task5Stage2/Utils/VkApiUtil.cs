using Task5Stage2.Models;
using Task5Stage2.RestApi;

namespace Task5Stage2.Utils;

public static class VkApiUtil
{
    public static int WallPost(string baseUrl, string method, string postMessage, string token, string apiVersion)
    {
        var parameters = new Dictionary<string, string>
        {
            { "message", postMessage },
            { "access_token", token },
            { "v", apiVersion },
        };

        var paramsContent = new FormUrlEncodedContent(parameters);

        var request = new RestRequest(baseUrl, method);
        request.AddContent(paramsContent);
        var response = RestClient.Post(request);
        var resultResponse = response.Deserialize<WallPostResult>();
        return resultResponse.response.post_id;
    }

    public static void WallPostComment(string baseUrl, string method, string postId, string commentMessage,
        string token, string apiVersion)
    {
        var parameters = new Dictionary<string, string>
        {
            { "post_id", postId },
            { "message", commentMessage },
            { "access_token", token },
            { "v", apiVersion },
        };

        var paramsContent = new FormUrlEncodedContent(parameters);

        var request = new RestRequest(baseUrl, method);
        request.AddContent(paramsContent);
        RestClient.Post(request);
    }

    public static void EditWallPostAddImage(string baseUrl, string methodEdit, string methodGetServer,
        string methodSave, string postId, string postMessage, string imgPath, string token,
        string apiVersion)
    {
        var image = LoadImage(baseUrl, methodGetServer, methodSave, imgPath, token, apiVersion);

        var parameters = new Dictionary<string, string>
        {
            { "post_id", postId },
            { "message", postMessage },
            { "access_token", token },
            { "v", apiVersion },
            { "attachments", $"photo{image[0].owner_id}_{image[0].id}" }
        };

        var paramsContent = new FormUrlEncodedContent(parameters);

        var request = new RestRequest(baseUrl, methodEdit);
        request.AddContent(paramsContent);
        RestClient.Post(request);
    }

    public static List<GetLikesResult.User> AddLikeToThePost(string baseUrl, string method, string postId, string token,
        string apiVersion)
    {
        var parameters = new Dictionary<string, string>
        {
            { "post_id", postId },
            { "access_token", token },
            { "v", apiVersion },
        };

        var paramsContent = new FormUrlEncodedContent(parameters);

        var request = new RestRequest(baseUrl, method);
        request.AddContent(paramsContent);
        var response = RestClient.Post(request);
        var resultResponse = response.Deserialize<GetLikesResult>();
        return resultResponse.response.users;
    }

    public static void DeleteWallPost(string baseUrl, string method, string postId, string token, string apiVersion)
    {
        var parameters = new Dictionary<string, string>
        {
            { "post_id", postId },
            { "access_token", token },
            { "v", apiVersion },
        };

        var paramsContent = new FormUrlEncodedContent(parameters);

        var request = new RestRequest(baseUrl, method);
        request.AddContent(paramsContent);
        RestClient.Post(request);
    }

    private static List<SavePhotoResult.Response> LoadImage(string baseUrl, string methodGetServer, string methodSave,
        string imgPath, string token, string apiVersion)
    {
        var parametersUploadServer = new Dictionary<string, string>
        {
            { "access_token", token },
            { "v", apiVersion }
        };

        var paramsUploadServerContent = new FormUrlEncodedContent(parametersUploadServer);

        var uploadServerRequest = new RestRequest(baseUrl, methodGetServer);
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

        var savePhotoRequest = new RestRequest(baseUrl, methodSave);
        savePhotoRequest.AddContent(paramsSavePhotoContent);
        var savePhotoResponse = RestClient.Post(savePhotoRequest);
        var resultSavePhotoResponse = savePhotoResponse.Deserialize<SavePhotoResult>();
        return resultSavePhotoResponse.response;
    }
}