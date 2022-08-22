namespace Task6Stage2.Models;

public struct PostData
{
    public int userId { get; set; }
    public int id { get; set; }
    public string title { get; set; }
    public string body { get; set; }
    
    public bool EqualsByData(PostData postData)
    {
        return postData.body == body
               && postData.title == title
               && postData.userId == userId;
    }
}