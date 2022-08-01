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
    public void Test1()
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
}

