using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Task5Stage2.RestApi;

public class RestResponse
{
    private HttpResponseMessage _responseMessage;

    public int StatusCode => (int)_responseMessage.StatusCode;
    private string ResponseString => _responseMessage.Content.ReadAsStringAsync().Result;

    public RestResponse(HttpResponseMessage responseMessage)
    {
        _responseMessage = responseMessage;
    }

   /* public bool IsJson()
    {
        return JsonUtil.TryToDeserializeObject<object>(ResponseString, out _);
    }*/
    
    public bool IsJsonEmpty()
    {
        return !JToken.Parse(ResponseString).HasValues;
    }

    public T Deserialize<T>()
    {
        var obj = JsonConvert.DeserializeObject<T>(ResponseString);
        return obj;
    }
}