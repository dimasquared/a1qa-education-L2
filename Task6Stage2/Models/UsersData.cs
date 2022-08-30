﻿namespace Task6Stage2.Models;

public struct Address
{
    public string street { get; set; }
    public string suite { get; set; }
    public string city { get; set; }
    public string zipcode { get; set; }
    public Geo geo { get; set; }
}

public struct Company
{
    public string name { get; set; }
    public string catchPhrase { get; set; }
    public string bs { get; set; }
}

public struct Geo
{
    public string lat { get; set; }
    public string lng { get; set; }
}

public struct UsersData
{
    public int id { get; set; }
    public string name { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public Address address { get; set; }
    public string phone { get; set; }
    public string website { get; set; }
    public Company company { get; set; }
}
