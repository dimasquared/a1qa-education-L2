using Task4Stage2.Models;
using Task4Stage2.RestApiFramework;
using Task4Stage2.RestApiFramework.Utils;

namespace Task4Stage2;

public class Tests
{
    private string baseUrl;
    private string postsUrl;
    private string postIdUrl;
    private string postNullIdUrl;
    private string usersUrl;
    private string userIdUrl;
    private PostData postDataById;
    private PostData sentPostData;
    private UsersData validUserData;
    private int userIndexForCheck;
    
    [SetUp]
    public void Setup()
    {
        JsonSettingsFileUtil jConfig = new JsonSettingsFileUtil(@"\Resources\config.json");
        baseUrl = jConfig.GetValue<string>("baseUrl");
        postsUrl = jConfig.GetValue<string>("postsUrl");
        postIdUrl = jConfig.GetValue<string>("postIdUrl");
        postNullIdUrl = jConfig.GetValue<string>("postNullIdUrl");
        usersUrl = jConfig.GetValue<string>("usersUrl");
        userIdUrl = jConfig.GetValue<string>("userIdUrl");
        
        JsonSettingsFileUtil jTestData = new JsonSettingsFileUtil(@"\Resources\testData.json");
        postDataById = jTestData.GetValue<PostData>("postDataById");
        sentPostData = jTestData.GetValue<PostData>("sentPostData");
        validUserData = jTestData.GetValue<UsersData>("validUserData");
        userIndexForCheck = jTestData.GetValue<int>("userIndexForCheck");
    }

    [Test, Order(1)]
    public void SendGetRequestToGetAllPosts()
    {
        RestClient client = new RestClient(baseUrl);
        RestRequest request = new RestRequest(postsUrl);
        RestResponse response = client.Get(request);
        Assert.AreEqual(200, response.StatusCode, "Request returned a non-200 response");
        Assert.IsTrue(response.IsJson(), "The list in response body is not json");

        var posts = response.Deserialize<PostData[]>();
        for (var i = 0; i < posts.Length - 1; i++)
        {
            var currentPost = posts[i];
            var nextPost = posts[i + 1];
            Assert.IsTrue(nextPost.id > currentPost.id, "Posts are not sorted ascending by id");
        }
    }

    [Test, Order(2)]
    public void SendGetRequestToGetPostWithId()
    {
        RestClient client = new RestClient(baseUrl);
        RestRequest request = new RestRequest(postIdUrl);
        RestResponse response = client.Get(request);
        Assert.AreEqual(200, response.StatusCode, "Request returned a non-200 response");

        var post = response.Deserialize<PostData>();
        Assert.AreEqual(postDataById.userId, post.userId, "userId is not correct");
        Assert.AreEqual(postDataById.id, post.id, "id is not correct");
        Assert.IsNotEmpty(post.title, "title is empty");
        Assert.IsNotEmpty(post.body, "body is empty");
    }

    [Test, Order(3)]
    public void SendGetRequestToGetPostWithIncorrectId()
    {
        RestClient client = new RestClient(baseUrl);
        RestRequest request = new RestRequest(postNullIdUrl);
        RestResponse response = client.Get(request);
        Assert.AreEqual(404, response.StatusCode, "Request returned a non-404 response");
        Assert.IsTrue(response.IsJsonEmpty(), "Response body is not empty");
    }

    [Test, Order(4)]
    public void SendPostRequestToCreatePost()
    {
        RestClient client = new RestClient(baseUrl);
        RestRequest request = new RestRequest(postsUrl);
        request.AddJsonBody(sentPostData);
        RestResponse response = client.Post(request);
        Assert.AreEqual(201, response.StatusCode, "Request returned a non-201 response");

        var post = response.Deserialize<PostData>();
        Assert.AreNotEqual(0, post.id, "id is not present in response");
        Assert.IsTrue(sentPostData.EqualsByData(post), "Post information is not correct");
    }

    [Test, Order(5)]
    public void SendGetRequestToGetUsers()
    {
        RestClient client = new RestClient(baseUrl);
        RestRequest request = new RestRequest(usersUrl);
        RestResponse response = client.Get(request);
        Assert.AreEqual(200, response.StatusCode, "Request returned a non-200 response");
        Assert.IsTrue(response.IsJson(), "The list in response body is not json");

        var users = response.Deserialize<UsersData[]>();
        var userId = users[userIndexForCheck-1];
        Assert.AreEqual(validUserData, userId, $"User (id={userIndexForCheck}) data does not equal to required data");
    }

    [Test, Order(6)]
    public void SendGetRequestToGetUserWithIdN()
    {
        RestClient client = new RestClient(baseUrl);
        RestRequest request = new RestRequest(userIdUrl);
        RestResponse response = client.Get(request);
        Assert.AreEqual(200, response.StatusCode, "Request returned a non-200 response");

        var user = response.Deserialize<UsersData>();
        Assert.AreEqual(validUserData, user, "User data does not equal to required data");
    }
}