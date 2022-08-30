using Newtonsoft.Json;

namespace Task6Stage2.DataBase.Models;

public class Project
{
    public int id { get; set; }
    public string name { get; set; }
    
    public override string ToString()
    {
        return nameof(Project) + ":\n" + JsonConvert.SerializeObject(this);
    }
}