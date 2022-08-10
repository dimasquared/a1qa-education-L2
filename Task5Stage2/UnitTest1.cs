using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
using Task5Stage2.Pages;
using Task5Stage2.Utils;

namespace Task5Stage2;

public class Tests
{
    private string url;
    private string password;
    private string token;
    private string login;
    private string apiVersion;
    private int userId;

    [SetUp]
    public void Setup()
    {
        ISettingsFile config = new JsonSettingsFile(Environment.CurrentDirectory + @"\Resources\config.json");
        url = config.GetValue<string>("ui.url");
        apiVersion = config.GetValue<string>("api.version");

        ISettingsFile testData = new JsonSettingsFile(Environment.CurrentDirectory + @"\Resources\testData.json");
        login = testData.GetValue<string>("userData.login");
        password = testData.GetValue<string>("userData.password");
        token = testData.GetValue<string>("userData.access_token");
        userId = testData.GetValue<int>("userData.userId");
    }

    [Test]
    public void Test1()
    {
        //steps 1-3
        AqualityServices.Browser.Maximize();
        AqualityServices.Browser.GoTo(url);
        var loginPage = new LoginPage();
        loginPage.InputLogin(login);
        loginPage.ClickSignInButton();
        var passwordPage = new PasswordPage();
        passwordPage.InputPassword(password);
        passwordPage.ClickContinueButton();
        var feedPage = new FeedPage();
        feedPage.ClickMyProfileButton();
        var myProfilePage = new MyProfilePage();
        Assert.IsTrue(myProfilePage.State.WaitForDisplayed(), "My Profile Page is not opened");
        
        //step 4-5
        var postMessage = TextUtil.RandomText();
        var postId = VkApiUtil.WallPost(postMessage, token, apiVersion).ToString();
        var postAuthor = myProfilePage.GetPostAuthor();
        var pageOwner = myProfilePage.GetPageOwner();
        Assert.AreEqual(pageOwner, postAuthor, "Name of post author is wrong");
        var messageOnTheWall = myProfilePage.GetMessageOnTheWall();
        Assert.AreEqual(postMessage, messageOnTheWall, "The message on the wall is wrong");
        
        //step 8-9
        var commentMessage = TextUtil.RandomText();
        VkApiUtil.WallPostComment(postId, commentMessage, token, apiVersion);
        myProfilePage.ShowNewComment();
        var commentAuthor = myProfilePage.GetPostCommentAuthor();
        Assert.AreEqual(pageOwner, commentAuthor, "Name of comment author is wrong");
        var commentToThePost = myProfilePage.GetPostCommentText();
        Assert.AreEqual(commentMessage, commentToThePost, "The comment text is wrong");
        
        //step 10-11
        myProfilePage.LikePost();
        var userWhoLikedId = VkApiUtil.AddLikeToThePost(postId, token, apiVersion);
        Assert.IsTrue(userWhoLikedId.Any(user => user.uid == userId), "There is no like from requested user");
        
        //step 12-13
        VkApiUtil.DeleteWallPost(postId, token, apiVersion);
        Assert.IsTrue(myProfilePage.CheckPostDeleted(), "The post was not deleted");
    }

   [TearDown]
    public void CleanUp()
    {
        if (AqualityServices.IsBrowserStarted)
        {
            AqualityServices.Browser.Quit();
        }
    }
}