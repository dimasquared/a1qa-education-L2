using Newtonsoft.Json.Linq;
using Task6Stage2.RestApiFramework.Utils;

namespace Task6Stage2.RestApiFramework;

public class RestResponse
{
    private HttpResponseMessage _responseMessage;

    public int StatusCode => (int)_responseMessage.StatusCode;
    private string ResponseString => _responseMessage.Content.ReadAsStringAsync().Result;

    public RestResponse(HttpResponseMessage responseMessage)
    {
        _responseMessage = responseMessage;
    }

    public bool IsJson()
    {
        return JsonUtil.TryToDeserializeObject<object>(ResponseString, out _);
    }
    
    public bool IsJsonEmpty()
    {
        return !JToken.Parse(ResponseString).HasValues;
    }

    public T Deserialize<T>()
    {
        JsonUtil.TryToDeserializeObject(ResponseString, out T obj);
        return obj;
    }
}