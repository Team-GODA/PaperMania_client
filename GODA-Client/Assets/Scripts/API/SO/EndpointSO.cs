using UnityEngine;

[CreateAssetMenu]
public class EndpointSO : ScriptableObject
{
    [field: SerializeField]
    public string BaseUrl { get; set; } = "http://gsmsv-1.yujun.kr:29859/api/v3/";

    [field: SerializeField]
    public string AuthEndPoint { get; set; } = "auth/";
    [field: SerializeField]
    public string LoginEndPoint { get; set; } = "login";
    [field: SerializeField]
    public string RegisterEndPoint { get; set; } = "register";
    [field: SerializeField]
    public string LogoutEndPoint { get; set; } = "logout";
    [Header("Data")]
    [field: SerializeField]
    public string DataEndPoint { get; set; } = "data/";
    [field: SerializeField]
    public string PlayerNameEndPoint { get; set; } = "name";
    [field: SerializeField]
    public string PlayerDataEndPoint { get; set; } = "player";
    [field: SerializeField]
    public string PlayerLevelEndPoint { get; set; } = "level";
}
