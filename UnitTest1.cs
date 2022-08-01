using Task4Stage2.Models;
using Task4Stage2.RestApiFramework;
using Task4Stage2.RestApiFramework.Utils;

namespace Task4Stage2;

public class Tests
{
    [Test, Order(1)]
    public void SendGetRequestToGetAllPosts()
    {
        RestClient client = new RestClient("https://jsonplaceholder.typicode.com");
        RestRequest request = new RestRequest("/posts");
        RestResponse response = client.Get(request);
        Assert.AreEqual(200, (int)response.StatusCode, "Request returned a non-200 response");
        Assert.IsTrue(response.IsJson(), "The list in response body is not json");

        var posts = response.Deserialize<PostData[]>();
        for (var i = 0; i < posts.Length - 1; i++)
        {
            var currentPost = posts[i];
            var nextPost = posts[i + 1];
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

    [Test, Order(3)]
    public void SendGetRequestToGetPostWithIncorrectId()
    {
        RestClient client = new RestClient("https://jsonplaceholder.typicode.com");
        RestRequest request = new RestRequest("/posts/150");

        RestResponse response = client.Get(request);
        Assert.AreEqual(404, (int)response.StatusCode, "Request returned a non-404 response");
        
        var posts = response.IsJsonEmpty();
        Assert.IsTrue(posts, "Response body is not empty");
    }

    [Test, Order(4)]
    public void SendPostRequestToCreatePost()
    {
        RestClient client = new RestClient("https://jsonplaceholder.typicode.com");
        RestRequest request = new RestRequest("/posts");

        var data = new PostData()
        {
            userId = 1,
            body = "dfvgjd9fj",
            title = "dfrdfderfd"
        };

        request.AddJsonBody(data);
        RestResponse response = client.Post(request);
        Assert.AreEqual(201, (int)response.StatusCode, "Request returned a non-201 response");

        var post = response.Deserialize<PostData>();
        Assert.AreNotEqual(0, post.id, "id is not present in response");
        Assert.IsTrue(data.EqualsByData(post), "Post information is not correct");
    }

    [Test, Order(5)]
    public void SendGetRequestToGetUsers()
    {
        RestClient client = new RestClient("https://jsonplaceholder.typicode.com");
        RestRequest request = new RestRequest("/users");
        RestResponse response = client.Get(request);
        Assert.AreEqual(200, (int)response.StatusCode, "Request returned a non-200 response");
        Assert.IsTrue(response.IsJson(), "The list in response body is not json");

        var users = response.Deserialize<UsersData[]>();
        var userId5 = users[4];
        var validUserData = new UsersData()
        {
            id = 5,
            email = "Lucio_Hettinger@annie.ca",
            name = "Chelsey Dietrich",
            username = "Kamren",
            phone = "(254)954-1289",
            website = "demarco.info",
            address = new Address()
            {
                city = "Roscoeview",
                street = "Skiles Walks",
                suite = "Suite 351",
                zipcode = "33263",
                geo = new Geo()
                {
                    lat = "-31.8129",
                    lng = "62.5342"
                }
            },
            company = new Company()
            {
                name = "Keebler LLC",
                catchPhrase = "User-centric fault-tolerant solution",
                bs = "revolutionize end-to-end systems"
            }
        };
        Assert.AreEqual(validUserData, userId5, "User (id=5) data does not equal to required data");
    }

    [Test, Order(6)]
    public void SendGetRequestToGetUserWithIdN()
    {
        RestClient client = new RestClient("https://jsonplaceholder.typicode.com");
        RestRequest request = new RestRequest("/users/5");
        RestResponse response = client.Get(request);
        Assert.AreEqual(200, (int)response.StatusCode, "Request returned a non-200 response");

        var user = response.Deserialize<UsersData>();
        var validUserData = new UsersData()
        {
            id = 5,
            email = "Lucio_Hettinger@annie.ca",
            name = "Chelsey Dietrich",
            username = "Kamren",
            phone = "(254)954-1289",
            website = "demarco.info",
            address = new Address()
            {
                city = "Roscoeview",
                street = "Skiles Walks",
                suite = "Suite 351",
                zipcode = "33263",
                geo = new Geo()
                {
                    lat = "-31.8129",
                    lng = "62.5342"
                }
            },
            company = new Company()
            {
                name = "Keebler LLC",
                catchPhrase = "User-centric fault-tolerant solution",
                bs = "revolutionize end-to-end systems"
            }
        };
        Assert.AreEqual(validUserData, user, "User data does not equal to required data");
    }
}