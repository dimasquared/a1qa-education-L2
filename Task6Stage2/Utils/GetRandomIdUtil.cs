using Task6Stage2.DataBase;

namespace Task6Stage2.Utils;

public static class GetRandomIdUtil
{
    public static List<int> GetRandomId()
    {
        List<int> testsId = new List<int>();
        List<int> chosenId = new List<int>();
        Random rnd = new Random();
        var testsCount = rnd.Next(10);

        using (TestDbContext db = new TestDbContext())
        {
            var AllTest = db.Test.ToList();
            foreach (var test in AllTest)
            {
                var id = test.id;
                var digitsId = id.ToString();
                if (digitsId.Length < 2) continue;
                for (int i = 0; i < digitsId.Length - 1; i++)
                {
                    if (digitsId[i] == digitsId[i + 1]) testsId.Add(id);
                }
            }
        }

        do
        {
            var index = rnd.Next(testsId.Count);
            chosenId.Add(testsId[index]);
            testsId.RemoveAt(index);
        } while (chosenId.Count < testsCount);

        return chosenId;
    }
}