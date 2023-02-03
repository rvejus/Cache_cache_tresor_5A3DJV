using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit.Forms;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class LobbyNetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField _crationRoomName;

    [SerializeField] private RoomItemUiScript _roomItemPrefab;
    [SerializeField] private Transform _roomListParent;
    
    [SerializeField] private RoomItemUiScript _playerItemPrefab;
    [SerializeField] private Transform _playerListParent;

    [SerializeField] private Text _statusField;
    [SerializeField] private Button _leaveButton;
    
    [SerializeField] private Button _startButton;
    
    private List<RoomItemUiScript> _roomList = new List<RoomItemUiScript>();
    private List<RoomItemUiScript> _playerList = new List<RoomItemUiScript>();
    
    void Start()
    {
        Initialize();
        Connect();
    }

    private void Initialize()
    {
        _leaveButton.interactable = false;
        _startButton.interactable = false;
    }

    #region Photon CallBack

    public override void OnConnectedToMaster()
    {
        Debug.Log(("Connecté au serveur principale"));
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Déconnecté");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Vous êtes dans le lobby !");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateRoomList(roomList);
    }
    
    public override void OnJoinedRoom()
    {
        _statusField.text = "Joined " + PhotonNetwork.CurrentRoom.Name;
        Debug.Log("Vous avez rejoin la Room : "+ PhotonNetwork.CurrentRoom.Name);
        _leaveButton.interactable = true;
        UpdatePlayerList();
        ClearRoomList();
    }

    public override void OnLeftRoom()
    {
        _statusField.text = "LOBBY";
        Debug.Log("Left Room");
        _leaveButton.interactable = false;
        _startButton.interactable = false;
        UpdatePlayerList();
       
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            _startButton.interactable = true;
        }

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
        _startButton.interactable = false;
    }

    #endregion
    
    private void Connect()
    {
        PhotonNetwork.NickName = "Player" + Random.Range(0, 5000);
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(_crationRoomName.text) == false)
        {
            PhotonNetwork.CreateRoom(_crationRoomName.text, new RoomOptions() {MaxPlayers = 2}, null);
        }
        
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    private void ClearRoomList()
    {
        for (int i =0; i< _roomList.Count; i++)
        {
            Destroy(_roomList[i].gameObject);
        }
        
        _roomList.Clear();
    }

    private void UpdateRoomList(List<RoomInfo> roomList)
    {
        ClearRoomList();

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].PlayerCount == 0)
            {
                continue;
            }

            RoomItemUiScript newRoomItem = Instantiate(_roomItemPrefab);
            newRoomItem.LobbyNetworkParent = this;
            newRoomItem.transform.SetParent(_roomListParent);
            newRoomItem.SetName(roomList[i].Name);

            _roomList.Add(newRoomItem);
        }
    }

    private void UpdatePlayerList()
    {
        for (int i =0; i< _playerList.Count; i++)
        {
            Destroy(_playerList[i].gameObject);
        }
        
        _playerList.Clear();
        
        if (PhotonNetwork.CurrentRoom == null) {return;}

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            RoomItemUiScript newPlayerItem = Instantiate(_playerItemPrefab);
            
            newPlayerItem.transform.SetParent(_playerListParent);
            newPlayerItem.SetName(player.Value.NickName);
            
            _playerList.Add(newPlayerItem);
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel("SampleScene");
    }
}
