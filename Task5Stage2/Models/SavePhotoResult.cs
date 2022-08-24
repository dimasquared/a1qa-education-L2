namespace Task5Stage2.Models;

public class SavePhotoResult
{
    public List<Response> response { get; set; }
    
    public class Response
    {
        public string id { get; set; }
        public int album_id { get; set; }
        public string owner_id { get; set; }
        public string photo { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string text { get; set; }
        public int date { get; set; }
        public string access_key { get; set; }
    }
}