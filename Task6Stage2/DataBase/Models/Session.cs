namespace Task6Stage2.DataBase.Models;

public class Session
{
    public int id { get; set; }
    public string session_key { get; set; }
    public DateTime created_time { get; set; }
    public int build_number { get; set; }
}