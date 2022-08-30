using Newtonsoft.Json;

namespace Task6Stage2.DataBase.Models;

public class Author
{
    public int id { get; set; }
    public string name { get; set; }
    public string login { get; set; }
    public string email { get; set; }
    
    public override string ToString()
    {
        return nameof(Author) + ":\n" + JsonConvert.SerializeObject(this);
    }
}