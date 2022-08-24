namespace Task5Stage2.RestApi;

public static class RestClient
{

    public static RestResponse Get(RestRequest request)
    {
        var url = request.BaseUrl + request.SubPath;
        using var httpClient = new HttpClient();
        using (HttpRequestMessage httpRequest = new HttpRequestMessage
                   { RequestUri = new Uri(url), Method = HttpMethod.Get })
        {
            HttpResponseMessage response = httpClient.SendAsync(httpRequest).Result;
            RestResponse result = new RestResponse(response);
            return result;
        }
    }

    public static RestResponse Post(RestRequest request)
    {
        var url = request.BaseUrl + request.SubPath;
        using var httpClient = new HttpClient();
        using (HttpRequestMessage httpRequest = new HttpRequestMessage
                   { Content = request.Data, RequestUri = new Uri(url), Method = HttpMethod.Post })
        {
            HttpResponseMessage response = httpClient.SendAsync(httpRequest).Result;
            RestResponse result = new RestResponse(response);
            return result;
        }
    }
}