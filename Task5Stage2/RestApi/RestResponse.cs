using System.Text;
using Newtonsoft.Json;

namespace Task5Stage2.RestApi;

public class RestResponse
{
    private HttpResponseMessage _responseMessage;

    public RestResponse(HttpResponseMessage responseMessage)
    {
        _responseMessage = responseMessage;
    }

    public enum EncodingType
    {
        utf_8, win1251
    }

    public T Deserialize<T>(EncodingType encoding = EncodingType.utf_8)
    {
        var obj = JsonConvert.DeserializeObject<T>(ResponseString(encoding));
        return obj;
    }
    
    private string ResponseString(EncodingType encoding = EncodingType.utf_8)
    {

        if (encoding == EncodingType.win1251)
        {
            var httpContent = _responseMessage.Content.ReadAsByteArrayAsync().Result;
            return Encoding.GetEncoding(1251).GetString(httpContent, 0, httpContent.Length);
        }

        return _responseMessage.Content.ReadAsStringAsync().Result;
    }
}