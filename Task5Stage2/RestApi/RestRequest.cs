namespace Task5Stage2.RestApi;

public class RestRequest
{
    private readonly string _baseUrl;
    private readonly string _subPath;
    private HttpContent _data;

    public HttpContent Data => _data;
    public string SubPath => _subPath;
    public string BaseUrl => _baseUrl;

    public RestRequest(string baseUrl, string subPath)
    {
        _baseUrl = baseUrl;
        _subPath = subPath;
    }

    public void AddContent(HttpContent obj)
    {
        _data = obj;
    }
}