using Task6Stage2.DataBase;
using Task6Stage2.DataBase.Models;
using Task6Stage2.RestApiFramework.Utils;
using Task6Stage2.Utils;

namespace Task6Stage2;

public class Test2
{
    private string projectName;
    private string testAuthorName;
    private string testAuthorEmail;
    private Test updatedDbEntry;
    private List<int> copiedEntriesId;
    private List<Test> copiedEntries = new();
    private int buildNumber;

    [SetUp]
    public void Setup()
    {
        JsonSettingsFileUtil jTestData = new JsonSettingsFileUtil(@"\Resources\testData.json");
        projectName = jTestData.GetValue<string>("projectNameTC2");
        testAuthorName = jTestData.GetValue<string>("testAuthorName");
        testAuthorEmail = jTestData.GetValue<string>("testAuthorEmail");
        buildNumber = jTestData.GetValue<int>("buildTC2");

        copiedEntriesId = GetRandomIdUtil.GetRandomId();
        Logger.GetInstance().Info("Entries id to copy: " + string.Join(", ", copiedEntriesId));

        var projectId = ProjectDb.GetProjectId(projectName);
        var authorId = AuthorDb.GetAuthorId(testAuthorName, testAuthorEmail);
        
        foreach (var id in copiedEntriesId)
        {
            var copiedEntry = TestsDb.Copy(id);
            copiedEntry.project_id = projectId;
            copiedEntry.author_id = authorId;
            var updCopiedEntry = TestsDb.Update(copiedEntry);
            copiedEntries.Add(updCopiedEntry);
        }
    }

    [Test]
    public void SimulateRunningTests()
    {
        foreach (var testEntry in copiedEntries)
        {
            var testStartTime = DateTime.Now;

            SimulateRunningTestUtil.SimulateRunningTest();
            Logger.GetInstance().Info($"Simulate running test {testEntry.id} done");

            var testEndTime = DateTime.Now;
            var testResultStatus = TestContext.CurrentContext.Result.Outcome.Status;
            var status = DataConverterUtils.GetTestStatus(testResultStatus);
            var testName = TestContext.CurrentContext.Test.Name;

            testEntry.name = testName;
            testEntry.status_id = (int?)status;
            testEntry.method_name = TestContext.CurrentContext.Test.MethodName;
            testEntry.session_id = SessionDb.AddSession(testStartTime, buildNumber).id;
            testEntry.start_time = testStartTime;
            testEntry.end_time = testEndTime;
            testEntry.env = Environment.MachineName;
            testEntry.browser = null;

            updatedDbEntry = TestsDb.Update(testEntry);
            Assert.IsTrue(updatedDbEntry.name == testName && updatedDbEntry.start_time == testStartTime,
                "Information does not updated");
        }
    }

    [TearDown]
    public void TearDown()
    {
        foreach (var testEntry in copiedEntries)
        {
            Assert.IsTrue(TestsDb.Delete(testEntry));
        }
    }
}