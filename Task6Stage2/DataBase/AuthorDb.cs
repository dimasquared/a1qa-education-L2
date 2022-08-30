using Task6Stage2.DataBase.Models;
using Task6Stage2.Utils;

namespace Task6Stage2.DataBase;

public static class AuthorDb
{
    public static int GetAuthorId(string testAuthorName, string testAuthorEmail)
    {
        using (TestDbContext db = new TestDbContext())
        {
            var author = db.Author.ToList()
                .FirstOrDefault(author => author.name == testAuthorName && author.email == testAuthorEmail);

            if (author == null)
            {
                author = new Author
                {
                    name = testAuthorName,
                    login = testAuthorEmail,
                    email = testAuthorEmail
                };
                db.Author.Add(author);
                db.SaveChanges();
                Logger.GetInstance().Info($"New author added to the database \n{author}");
            }

            return author.id;
        }
    }
}