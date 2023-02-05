using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class CityPlayer : MonoBehaviourPunCallbacks
{
    public int playerID;
    public bool isReady;
    public Player photonPlayer;

    [PunRPC]
    public void Initialize(Player player) {
        photonPlayer = player;
        playerID = player.ActorNumber-1;
        Debug.Log(playerID);
        GameManager.Instance.players[playerID] = this;
        isReady = false;
    }

    private void Start()
    {
        if (!photonView.IsMine) { return; }

        UIControl.Instance.CurrentPlayer = this;
    }


    public void callAim()
    {
        RaycastHit hit;
        Vector3 fwd = Aim.Instance.transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, out hit, 10))
        {
            photonView.RPC("CallShoot", RpcTarget.All, playerID,hit);
        }
    }
    
    [PunRPC]
    public void CallShoot(int IDPlayer, RaycastHit hit)
    {
        Aim.Instance.Shoot(playerID, hit);
    }
}
