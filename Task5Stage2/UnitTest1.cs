using System.Text;
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
    private string imagePath;
    private int userId;

    [SetUp]
    public void Setup()
    {
        ISettingsFile config = new JsonSettingsFile(Environment.CurrentDirectory + @"\Resources\config.json");
        url = config.GetValue<string>("url");

        ISettingsFile testData = new JsonSettingsFile(Environment.CurrentDirectory + @"\Resources\testData.json");
        login = testData.GetValue<string>("userData.login");
        password = testData.GetValue<string>("userData.password");
        token = testData.GetValue<string>("userData.access_token");
        userId = testData.GetValue<int>("userData.userId");
        imagePath = Environment.CurrentDirectory + testData.GetValue<string>("imagePath");

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

    [Test]
    public void Test1()
    {
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
        feedPage.ClickMyProfileButton();
        var myProfilePage = new MyProfilePage();
        Assert.IsTrue(myProfilePage.State.WaitForDisplayed(), "My Profile Page is not opened");

        var postMessage = TextUtil.RandomText();
        var postId = VkApiUtil.WallPost(postMessage, token).ToString();
        var postAuthor = myProfilePage.GetPostAuthor();
        var pageOwner = myProfilePage.GetPageOwner();
        Assert.AreEqual(pageOwner, postAuthor, "Name of post author is wrong");
        var messageOnTheWall = myProfilePage.GetMessageOnTheWall();
        Assert.AreEqual(postMessage, messageOnTheWall, "The message on the wall is wrong");

        var newPostMessage = TextUtil.RandomText();
        VkApiUtil.EditWallPostAddImage(postId, newPostMessage, imagePath, token);
        myProfilePage.WaitForEditPostLoad();
        var newMessageOnTheWall = myProfilePage.GetMessageOnTheWall();
        Assert.AreEqual(newPostMessage, newMessageOnTheWall, "The message on the wall was not edited");
        var imageUrl = myProfilePage.GetImageUrlFromTheWall();
        Assert.IsTrue(CompareImagesUtil.CompareImages(imagePath, imageUrl), "The image on the wall is wrong");

        var commentMessage = TextUtil.RandomText();
        VkApiUtil.WallPostComment(postId, commentMessage, token);
        myProfilePage.ShowNewComment();
        var commentAuthor = myProfilePage.GetPostCommentAuthor();
        Assert.AreEqual(pageOwner, commentAuthor, "Name of comment author is wrong");
        var commentToThePost = myProfilePage.GetPostCommentText();
        Assert.AreEqual(commentMessage, commentToThePost, "The comment text is wrong");

        myProfilePage.LikePost();
        var userWhoLikedId = VkApiUtil.AddLikeToThePost(postId, token);
        Assert.IsTrue(userWhoLikedId.Any(user => user.uid == userId), "There is no like from requested user");

        VkApiUtil.DeleteWallPost(postId, token);
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