using NUnit.Framework.Interfaces;
using Task6Stage2.DataBase;

namespace Task6Stage2.Utils;

public static class DataConverterUtils
{
    public static (string name, string email) GetTestAuthorData(string testAuthor)
    {
        var openAngelBracket = testAuthor.IndexOf('<');
        var testAuthorName = testAuthor.Substring(0, openAngelBracket - 1);
        var testAuthorEmail = testAuthor.Substring(openAngelBracket + 1, testAuthor.Length - openAngelBracket - 2);
        return (name: testAuthorName, email: testAuthorEmail);
    }

    public static TestResultStatusEnum GetTestStatus(TestStatus testResultStatus)
    {
        TestResultStatusEnum status;
        switch (testResultStatus)
        {
            case TestStatus.Inconclusive:
                status = TestResultStatusEnum.FAILED;
                break;
            case TestStatus.Skipped:
                status = TestResultStatusEnum.SKIPPED;
                break;
            case TestStatus.Passed:
                status = TestResultStatusEnum.PASSED;
                break;
            case TestStatus.Warning:
                status = TestResultStatusEnum.FAILED;
                break;
            case TestStatus.Failed:
                status = TestResultStatusEnum.FAILED;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        return status;
    }
}