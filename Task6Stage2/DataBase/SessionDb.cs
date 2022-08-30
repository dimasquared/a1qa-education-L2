using Task6Stage2.DataBase.Models;
using Task6Stage2.Utils;

namespace Task6Stage2.DataBase;

public static class SessionDb
{
    public static Session AddSession(DateTime testStartTime, int buildNumber)
    {
        using (TestDbContext db = new TestDbContext())
        {
            Session session = new Session
            {
                session_key = testStartTime.ToString(),
                created_time = testStartTime,
                build_number = buildNumber
            };

            db.Session.Add(session);
            db.SaveChanges();
            Logger.GetInstance().Info($"New session added to the database \n{session}");
            return session;
        }
    }
}