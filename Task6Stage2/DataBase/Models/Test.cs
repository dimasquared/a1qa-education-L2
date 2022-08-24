namespace Task6Stage2.DataBase.Models;

public class Test
{
    public int id { get; set; }
    public string name { get; set; }
    public int? status_id { get; set; }
    public string method_name { get; set; }
    public int project_id { get; set; }
    public int session_id { get; set; }
    public DateTime? start_time { get; set; }
    public DateTime? end_time { get; set; }
    public string env { get; set; }
    public string? browser { get; set; }
    public int? author_id { get; set; }
}