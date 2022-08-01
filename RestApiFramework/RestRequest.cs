namespace Task4Stage2.RestApiFramework;

public class RestRequest
{
    private readonly string _subPath;

    public string SubPath => _subPath;

    public RestRequest(string subPath)
    {
        _subPath = subPath;
    }
    
    
}