namespace Task4Stage2.RestApiFramework;

public class RestClient
{
    private string _baseUrl;
    private HttpClient _httpClient;

    public RestClient(string baseUrl)
    {
        _baseUrl = baseUrl;
        _httpClient = new HttpClient();
    }

    public RestResponse Get(RestRequest request)
    {
        var url = _baseUrl + request.SubPath;
        using (HttpRequestMessage httpRequest = new HttpRequestMessage
                   { RequestUri = new Uri(url), Method = HttpMethod.Get })
        {
            HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
            RestResponse result = new RestResponse(response);
            return result;
        }
    }

    public RestResponse Post(RestRequest request)
    {
        var url = _baseUrl + request.SubPath;
        using (HttpRequestMessage httpRequest = new HttpRequestMessage
                   { Content = request.Data, RequestUri = new Uri(url), Method = HttpMethod.Post })
        {
            HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
            RestResponse result = new RestResponse(response);
            return result;
        }
    }
    
    ~RestClient()
    {
        _httpClient.Dispose();
    }
}