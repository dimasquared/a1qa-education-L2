using Microsoft.EntityFrameworkCore;
using Task6Stage2.DataBase.Models;

namespace Task6Stage2.DataBase;

public class DbCrud : DbContext
{
    public static void TestAdd(string testName, string methodName, Session session, DateTime testStartTime,
        DateTime testEndTime, TestResultStatusEnum status, string environment, string testAuthorName, string testAuthorEmail)
    {
        using (TestDbContext db = new TestDbContext())
        {
            var project = GetProject(db);
            var author = GetAuthor(db, testAuthorName, testAuthorEmail);

            db.Session.Add(session);
            db.SaveChanges();

            Test test1 = new Test
            {
                name = testName,
                status_id = (int?)status,
                method_name = methodName,
                project_id = project.id,
                session_id = session.id,
                start_time = testStartTime,
                end_time = testEndTime,
                env = environment,
                browser = null,
                author_id = author.id
            };

            db.Test.Add(test1);
            db.SaveChanges();
        }
    }

    public static void TestCheckAdding()
    {
    }

    private static Project GetProject(TestDbContext db)
    {
        var project = db.Project.ToList().FirstOrDefault(project => project.name == "Rest API");

        if (project == null)
        {
            project = new Project { name = "Rest API" };
            db.Project.Add(project);
            db.SaveChanges();
        }

        return project;
    }
    
    private static Author GetAuthor(TestDbContext db, string testAuthorName, string testAuthorEmail)
    {
        var author = db.Author.ToList().FirstOrDefault(author => author.name == testAuthorName && author.email == testAuthorEmail);

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
        }

        return author;
    }
}