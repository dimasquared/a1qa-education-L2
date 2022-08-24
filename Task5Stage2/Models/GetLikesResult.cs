namespace Task5Stage2.Models;

public class GetLikesResult
{
    public Response response { get; set; }
    
    public class Response
    {
        public int count { get; set; }
        public List<User> users { get; set; }
    }
    
    public class User
    {
        public int uid { get; set; }
        public int copied { get; set; }
    }
}