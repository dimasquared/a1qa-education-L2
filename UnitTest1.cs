using Task4Stage2.APILibrary;
using Task4Stage2.Models;

namespace Task4Stage2;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        HttpResponseMessage response = GetResponse.GetPosts();
        Assert.AreEqual(200, (int)response.StatusCode, "Request returned a non-200 response");
        
        var responseString = response.Content.ReadAsStringAsync().Result;
        var isJson = JsonUtil.TryToDeserializeObject(responseString, out PostData[] posts);
        Assert.IsTrue(isJson, "The list in response body is not json");

        for (var i = 0; i < posts.Length; i++)
        {
            var currentPost = posts[i];
            var nextPost = posts[i+1];
            Assert.IsTrue((nextPost.id > currentPost.id), "Posts are not sorted ascending by id");
        }
    }
}

