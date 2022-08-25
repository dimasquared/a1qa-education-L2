namespace Task6Stage2.Utils;

public static class GetTestAuthorDataUtil
{
    public static (string name, string email) GetAuthorData(string testAuthor)
    {
        var openAngelBracket = testAuthor.IndexOf('<');
        var testAuthorName = testAuthor.Substring(0, openAngelBracket - 1);
        var testAuthorEmail = testAuthor.Substring(openAngelBracket + 1, testAuthor.Length - openAngelBracket - 2);
        return (name: testAuthorName, email: testAuthorEmail);
    }
}