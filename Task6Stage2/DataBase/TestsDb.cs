using Microsoft.EntityFrameworkCore;
using Task6Stage2.Utils;
using Test = Task6Stage2.DataBase.Models.Test;

namespace Task6Stage2.DataBase;

public class TestsDb : DbContext
{
    public static Test Add(Test addingDbEntry)
    {
        using (TestDbContext db = new TestDbContext())
        {
            db.Test.Add(addingDbEntry);
            db.SaveChanges();
            
            var addedEntry = db.Test.Find(addingDbEntry.id);
            Logger.GetInstance().Info($"Entity added to the database \n{addedEntry}");
            return addedEntry;
        }
    }

    public static Test Copy(int id)
    {
        using (TestDbContext db = new TestDbContext())
        {
            var copiedEntry = db.Test.Find(id);
            Logger.GetInstance().Info($"Founded entity to copy \n{copiedEntry}");
            copiedEntry.id = 0;
            db.Test.Add(copiedEntry);
            db.SaveChanges();
            
            Logger.GetInstance().Info($"Entity copied with id = {copiedEntry.id}");
            return copiedEntry;
        }
    }

    public static Test Update(Test updatedEntry)
    {
        using (TestDbContext db = new TestDbContext())
        {
            db.Test.Attach(updatedEntry);
            db.Entry(updatedEntry).State = EntityState.Modified;
            db.SaveChanges();
            
            var editedEntry = db.Test.Find(updatedEntry.id);
            Logger.GetInstance().Info($"Entity updated \n{editedEntry}");
            return editedEntry;
        }
    }

    public static bool Delete(Test testEntry)
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
}