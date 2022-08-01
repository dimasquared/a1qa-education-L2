using Task4Stage2.Models;
using Task4Stage2.RestApiFramework;
using Task4Stage2.RestApiFramework.Utils;

namespace Task4Stage2;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test, Order(1)]
    public void SendGetRequestToGetAllPosts()
    {
        RestClient client = new RestClient("https://jsonplaceholder.typicode.com");
        RestRequest request = new RestRequest("/posts");
        RestResponse response = client.Get(request);
        Assert.AreEqual(200, (int)response.StatusCode, "Request returned a non-200 response");
        Assert.IsTrue(response.IsJson(), "The list in response body is not json");

        var posts = response.Deserialize<PostData[]>();
        for (var i = 0; i < posts.Length-1; i++)
        {
            var currentPost = posts[i];
            var nextPost = posts[i+1];
            Assert.IsTrue((nextPost.id > currentPost.id), "Posts are not sorted ascending by id");
        }
    }

    [Test, Order(2)]
    public void SendGetRequestToGetPostWithId()
    {
        RestClient client = new RestClient("https://jsonplaceholder.typicode.com");
        RestRequest request = new RestRequest("/posts/99");
        RestResponse response = client.Get(request);
        Assert.AreEqual(200, (int)response.StatusCode, "Request returned a non-200 response");
        
        var post = response.Deserialize<PostData>();
        Assert.AreEqual(10, post.userId, "userId is not correct");
        Assert.AreEqual(99, post.id, "id is not correct");
        Assert.IsNotEmpty(post.title, "title is empty");
        Assert.IsNotEmpty(post.body, "body is empty");
    }
}

