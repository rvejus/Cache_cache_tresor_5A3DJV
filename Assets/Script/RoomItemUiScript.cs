using UnityEngine;
using UnityEngine.UI;

public class RoomItemUiScript : MonoBehaviour
{
    public LobbyNetworkManager LobbyNetworkParent;
    [SerializeField] private Text _roomName;


    public void SetName(string roomName)
    {
        _roomName.text = roomName;
    }

    public void OnJoinPressed()
    {
        LobbyNetworkParent.JoinRoom(_roomName.text);
    }
}
