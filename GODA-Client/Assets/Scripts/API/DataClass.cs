using System;
using Newtonsoft.Json;

public class UserData
{
    [JsonProperty("id")]
    public int Id { get; set; }
}
