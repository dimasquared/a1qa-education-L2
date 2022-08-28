using Microsoft.EntityFrameworkCore;
using Task6Stage2.DataBase.Models;
using Task6Stage2.Utils;
using Test = Task6Stage2.DataBase.Models.Test;

namespace Task6Stage2.DataBase;

public class DbCrud : DbContext
{
    public static Test TestAdd(string projectName, string testName, string methodName, Session session,
        DateTime testStartTime, DateTime testEndTime, TestResultStatusEnum status, string environment,
        string testAuthorName, string testAuthorEmail)
    {
        using (TestDbContext db = new TestDbContext())
        {
            var project = GetProject(projectName, db);
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
            
            var addedEntry = db.Test.Find(test1.id);
            Logger.GetInstance().Info($"Entity added to the database \n{addedEntry}");
            return addedEntry;
        }
    }

    public static Test TestCopy(int id, string projectName, string testAuthorName, string testAuthorEmail)
    {
        using (TestDbContext db = new TestDbContext())
        {
            var foundedEntry = db.Test.Find(id);
            Logger.GetInstance().Info($"Founded entity to copy \n{foundedEntry}");
            foundedEntry.id = 0;
            db.Test.Add(foundedEntry);
            db.SaveChanges();
            
            var copiedEntry = db.Test.Find(foundedEntry.id);

            var author = GetAuthor(db, testAuthorName, testAuthorEmail);
            copiedEntry.author_id = author.id;

            var project = GetProject(projectName, db);
            copiedEntry.project_id = project.id;

            db.SaveChanges();
            Logger.GetInstance().Info($"Entity with the id {id} copied with an indication of the current project and the author \n{copiedEntry}");
            return copiedEntry;
        }
    }

    public static Test TestUpdate(Test dbEntry, string testName, string methodName, Session session,
        DateTime testStartTime, DateTime testEndTime, TestResultStatusEnum status, string environment)
    {
        using (TestDbContext db = new TestDbContext())
        {
            var editingDbEntry = db.Test.Find(dbEntry.id);
            Logger.GetInstance().Info($"Entity to update \n{editingDbEntry}");

            db.Session.Add(session);
            db.SaveChanges();

            editingDbEntry.name = testName;
            editingDbEntry.status_id = (int?)status;
            editingDbEntry.method_name = methodName;
            editingDbEntry.session_id = session.id;
            editingDbEntry.start_time = testStartTime;
            editingDbEntry.end_time = testEndTime;
            editingDbEntry.env = environment;
            editingDbEntry.browser = null;

            db.SaveChanges();
            
            Logger.GetInstance().Info($"Entity updated \n{editingDbEntry}");
            return editingDbEntry;
        }
    }

    public static bool TestDelete(Test testEntry)
    {
        using (TestDbContext db = new TestDbContext())
        {
            var deletingDbEntry = db.Test.Find(testEntry.id);
            Logger.GetInstance().Info($"Entity to delete \n{deletingDbEntry}");

            db.Test.Remove(deletingDbEntry);
            db.SaveChanges();

            return db.Test.Find(deletingDbEntry.id) == null;
        }
    }

    private static Project GetProject(string projectName, TestDbContext db)
    {
        var project = db.Project.ToList().FirstOrDefault(project => project.name == projectName);

        if (project == null)
        {
            project = new Project { name = projectName };
            db.Project.Add(project);
            db.SaveChanges();
        }

        return project;
    }

    private static Author GetAuthor(TestDbContext db, string testAuthorName, string testAuthorEmail)
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
        }

        return author;
    }
}