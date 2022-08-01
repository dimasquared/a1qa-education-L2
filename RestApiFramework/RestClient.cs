namespace Task4Stage2.RestApiFramework;

public class RestClient : IDisposable
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

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    ~RestClient()
    {
        throw new NotImplementedException();
    }
}