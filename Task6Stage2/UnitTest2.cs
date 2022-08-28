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

    [SetUp]
    public void Setup()
    {
        JsonSettingsFileUtil jTestData = new JsonSettingsFileUtil(@"\Resources\testData.json");
        projectName = jTestData.GetValue<string>("projectNameTC2");
        testAuthorName = jTestData.GetValue<string>("testAuthorName");
        testAuthorEmail = jTestData.GetValue<string>("testAuthorEmail");

        copiedEntriesId = GetRandomIdUtil.GetRandomId();

        foreach (var id in copiedEntriesId)
        {
            copiedEntries.Add(DbCrud.TestCopy(id, projectName, testAuthorName, testAuthorEmail));
        }
    }

    [Test]
    public void SimulateRunningTests()
    {
        var testName = TestContext.CurrentContext.Test.Name;
        var methodName = TestContext.CurrentContext.Test.MethodName;
        var environment = Environment.MachineName;
        var testResultStatus = TestContext.CurrentContext.Result.Outcome.Status;

        foreach (var testEntry in copiedEntries)
        {
            var testStartTime = DateTime.Now;

            SimulateRunningTestUtil.SimulateRunningTest();

            var testEndTime = DateTime.Now;
            var status = DataConverterUtils.GetTestStatus(testResultStatus);

            Session session = new Session
            {
                session_key = testStartTime.ToString(),
                created_time = testStartTime,
                build_number = 11
            };

            updatedDbEntry = DbCrud.TestUpdate(testEntry, testName, methodName, session, testStartTime,
                testEndTime, status, environment);
            Assert.IsTrue(updatedDbEntry.name == testName && updatedDbEntry.start_time == testStartTime,
                "Information does not updated");
        }
    }

    [TearDown]
    public void TearDown()
    {
        foreach (var testEntry in copiedEntries)
        {
            Assert.IsTrue(DbCrud.TestDelete(testEntry));
        }
    }
}