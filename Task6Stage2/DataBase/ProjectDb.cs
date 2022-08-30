using Task6Stage2.DataBase.Models;
using Task6Stage2.Utils;

namespace Task6Stage2.DataBase;

public static class ProjectDb
{
    public static int GetProjectId(string projectName)
    {
        using (TestDbContext db = new TestDbContext())
        {
            var project = db.Project.ToList().FirstOrDefault(project => project.name == projectName);

            if (project == null)
            {
                project = new Project { name = projectName };
                db.Project.Add(project);
                db.SaveChanges();
                Logger.GetInstance().Info($"New project added to the database \n{project}");
            }

            return project.id;
        }
    }
}