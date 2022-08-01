﻿using System.Text;
using Newtonsoft.Json;

namespace Task4Stage2.RestApiFramework;

public class RestRequest
{
    private readonly string _subPath;
    private StringContent _data;

    public StringContent Data => _data;
    public string SubPath => _subPath;

    public RestRequest(string subPath)
    {
        _subPath = subPath;
    }

    public void AddJsonBody<T>(T obj)
    {
        var json = JsonConvert.SerializeObject(obj);
        _data = new StringContent(json, Encoding.UTF8, "application/json");
    }
    
}