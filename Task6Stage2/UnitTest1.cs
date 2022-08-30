using Task6Stage2.DataBase;
using Task6Stage2.DataBase.Models;
using Task6Stage2.Models;
using Task6Stage2.RestApiFramework;
using Task6Stage2.RestApiFramework.Utils;
using Task6Stage2.Utils;

namespace Task6Stage2;

public class Tests
{
    private string baseUrl;
    private string postsUrl;
    private string usersUrl;
    private PostData postDataById;
    private PostData sendPostData;
    private UsersData validUserData;
    private int userIndexForCheck;
    private int postIndexForCheck;
    private int postIncorrectIndexForCheck;
    private DateTime testStartTime;
    private string projectName;
    private int buildNumber;

    [SetUp]
    public void Setup()
    {
        JsonSettingsFileUtil jConfig = new JsonSettingsFileUtil(@"\Resources\config.json");
        baseUrl = jConfig.GetValue<string>("baseUrl");
        postsUrl = jConfig.GetValue<string>("postsUrl");
        usersUrl = jConfig.GetValue<string>("usersUrl");

        JsonSettingsFileUtil jTestData = new JsonSettingsFileUtil(@"\Resources\testData.json");
        postDataById = jTestData.GetValue<PostData>("postDataById");
        sendPostData = jTestData.GetValue<PostData>("sendPostData");
        validUserData = jTestData.GetValue<UsersData>("validUserData");
        userIndexForCheck = jTestData.GetValue<int>("userIndexForCheck");
        postIndexForCheck = jTestData.GetValue<int>("postIndexForCheck");
        postIncorrectIndexForCheck = jTestData.GetValue<int>("postIncorrectIndexForCheck");
        projectName = jTestData.GetValue<string>("projectNameTC1");
        buildNumber = jTestData.GetValue<int>("buildTC1");
        
        testStartTime = DateTime.Now;
    }

    [Test, Order(1)]
    [Author("Harry Potter", "potter@hogwarts.net")]
    public void SendGetRequestToGetAllPosts()
    {
        RestRequest request = new RestRequest(baseUrl, postsUrl);
        RestResponse response = RestClient.Get(request);
        Assert.AreEqual(200, response.StatusCode, "Status code is not 200");
        Assert.IsTrue(response.IsJson(), "The list in response body is not json");

        var posts = response.Deserialize<PostData[]>();
        Assert.IsTrue(SortUtil.CheckAscendingSortUtil(posts, post => post.id), "Posts are not sorted ascending by id");
    }

    [Test, Order(2)]
    [Author("Harry Potter", "potter@hogwarts.net")]
    public void SendGetRequestToGetPostWithId()
    {
        RestRequest request = new RestRequest(baseUrl, postsUrl + $"//{postIndexForCheck}");
        RestResponse response = RestClient.Get(request);
        Assert.AreEqual(200, response.StatusCode, "Status code is not 200");

        var post = response.Deserialize<PostData>();
        Assert.AreEqual(postDataById.userId, post.userId, "userId is not correct");
        Assert.AreEqual(postDataById.id, post.id, "id is not correct");
        Assert.IsNotEmpty(post.title, "title is empty");
        Assert.IsNotEmpty(post.body, "body is empty");
    }

    [Test, Order(3)]
    [Author("Harry Potter", "potter@hogwarts.net")]
    public void SendGetRequestToGetPostWithIncorrectId()
    {
        RestRequest request = new RestRequest(baseUrl, postsUrl + $"//{postIncorrectIndexForCheck}");
        RestResponse response = RestClient.Get(request);
        Assert.AreEqual(404, response.StatusCode, "Status code is not 404");
        Assert.IsTrue(response.IsJsonEmpty(), "Response body is not empty");
    }

    [Test, Order(4)]
    [Author("Harry Potter", "potter@hogwarts.net")]
    public void SendPostRequestToCreatePost()
    {
        RestRequest request = new RestRequest(baseUrl, postsUrl);
        request.AddJsonBody(sendPostData);
        RestResponse response = RestClient.Post(request);
        Assert.AreEqual(201, response.StatusCode, "Status code is not 201");

        var post = response.Deserialize<PostData>();
        Assert.AreNotEqual(0, post.id, "id is not present in response");
        Assert.IsTrue(sendPostData.EqualsByData(post), "Post information is not correct");
    }

    [Test, Order(5)]
    [Author("Harry Potter", "potter@hogwarts.net")]
    public void SendGetRequestToGetUsers()
    {
        RestRequest request = new RestRequest(baseUrl, usersUrl);
        RestResponse response = RestClient.Get(request);
        Assert.AreEqual(200, response.StatusCode, "Status code is not 200");
        Assert.IsTrue(response.IsJson(), "The list in response body is not json");

        var users = response.Deserialize<UsersData[]>();
        var userId = users[userIndexForCheck - 1];
        Assert.AreEqual(validUserData, userId, $"User (id={userIndexForCheck}) data does not equal to required data");
    }

    [Test, Order(6)]
    [Author("Harry Potter", "potter@hogwarts.net")]
    public void SendGetRequestToGetUserWithIdN()
    {
        RestRequest request = new RestRequest(baseUrl, usersUrl + $"//{userIndexForCheck}");
        RestResponse response = RestClient.Get(request);
        Assert.AreEqual(200, response.StatusCode, "Status code is not 200");

        var user = response.Deserialize<UsersData>();
        Assert.AreEqual(validUserData, user, "User data does not equal to required data");
    }

    [TearDown]
    public void TearDown()
    {
        var testName = TestContext.CurrentContext.Test.Name;
        var testAuthor = (string)TestContext.CurrentContext.Test.Properties.Get("Author");
        var testAuthorData = DataConverterUtils.GetTestAuthorData(testAuthor);
        
        var testResultStatus = TestContext.CurrentContext.Result.Outcome.Status;
        var status = DataConverterUtils.GetTestStatus(testResultStatus);

        Test addingDbEntry = new Test
        {
            name = testName,
            status_id = (int?)status,
            method_name = TestContext.CurrentContext.Test.MethodName,
            project_id = ProjectDb.GetProjectId(projectName),
            session_id = SessionDb.AddSession(testStartTime, buildNumber).id,
            start_time = testStartTime,
            end_time = DateTime.Now,
            env = Environment.MachineName,
            author_id = AuthorDb.GetAuthorId(testAuthorData.name, testAuthorData.email)
        };
        
        var addedDbEntry = TestsDb.Add(addingDbEntry);
        Assert.IsTrue(addedDbEntry.name == testName && addedDbEntry.start_time == testStartTime, "Information does not added");
    }
}