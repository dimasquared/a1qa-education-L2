namespace Task5Stage2.Models;

public class WallPostResult
{
    public Response response { get; set; }
    
    public class Response
    {
        public int post_id { get; set; }
    }
}