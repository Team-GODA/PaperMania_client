using UnityEngine;
using UnityEngine.UI;

public class SetName : MonoBehaviour
{
    public InputField text;
    public EndpointSO endpointSO;

    public void NameUpdate()
    {
        int id = PlayerPrefs.GetInt("Id");

        Name newName = new Name
        {
            playerName = text.text
        };

        APIConnector.instance.Post<Response<Name>>(endpointSO.PlayerDataEndPoint, newName, (user) =>
        {
            Debug.Log("New Name : " + user.Data.playerName);
        }, (user) =>
        {
            Debug.Log(user);
        }, true);
    }
}
