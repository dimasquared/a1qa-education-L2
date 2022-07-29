namespace Task4Stage2.APILibrary;

public class GetResponse
{
    private static string _baseUrl = "https://jsonplaceholder.typicode.com";

    public static HttpResponseMessage GetPosts()
    {
        var postsUrl = _baseUrl + "/posts";
        using (var httpClient = new HttpClient())
        {
            using (HttpRequestMessage request = new HttpRequestMessage
                       { RequestUri = new Uri(postsUrl), Method = HttpMethod.Get })
            {
                var response = httpClient.SendAsync(request).Result;
                return response;
            }
        }
    }
}