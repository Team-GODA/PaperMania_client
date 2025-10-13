using UnityEngine;

[CreateAssetMenu]
public class EndpointSO : ScriptableObject
{
    [field: SerializeField]
    public string BaseUrl { get; set; } = "http://gsmsv-1.yujun.kr:29859/api/v3/";

    [field: SerializeField]
    public string AuthEndPoint { get; set; } = "auth/";
    [field: SerializeField]
    public string LoginEndPoint { get; set; } = "auth/login";
    [field: SerializeField]
    public string RegisterEndPoint { get; set; } = "auth/register";
    [field: SerializeField]
    public string LogoutEndPoint { get; set; } = "auth/logout";
    [field: SerializeField]
    public string PlayerNameEndPoint { get; set; } = "data/name";
    [field: SerializeField]
    public string PlayerLevelEndPoint { get; set; } = "data/level";
    [field: SerializeField]
    public string PlayerDataEndPoint { get; set; } = "data/player";
}
