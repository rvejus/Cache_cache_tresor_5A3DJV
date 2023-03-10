using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {
   
    public static UIControl Instance;
    public Button _aimBtn;
    public Button _validateBtn;
    public Button _debugTimerBtn;
    public CityPlayer CurrentPlayer;
 
    private void Awake() {
        Instance = this;
    }
   
    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient) _debugTimerBtn.gameObject.SetActive(false);
        if (GameManager.Instance.playersObjects[CurrentPlayer.playerID] == null) _validateBtn.interactable = false;
        else _validateBtn.interactable = true;
    }
   
    public void OnAimButtonPressed() {
        if (CurrentPlayer == null) { return; }
        CurrentPlayer.callAim();
    }

    public void OnValidateButtonPressed()
    {
        if (CurrentPlayer == null) { return; }
        CurrentPlayer.photonView.RPC("SetReady",RpcTarget.All);
        
    }
}
