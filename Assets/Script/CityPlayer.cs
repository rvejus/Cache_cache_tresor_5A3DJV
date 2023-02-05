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
        Transform viseur= Aim.Instance.transform;
        float tx = viseur.transform.position.x;
        float ty = viseur.transform.position.y;
        float tz = viseur.transform.position.z;
        float rx = viseur.transform.position.x;
        float ry = viseur.transform.position.y;
        float rz = viseur.transform.position.z;
        photonView.RPC("CallShoot", RpcTarget.All, playerID,tx,ty,tz,rx,ry,rz);
        
    }
    
    [PunRPC]
    public void CallShoot(int IDPlayer, float tx, float ty, float tz, float rx, float ry, float rz)
    {
        GameObject go = new GameObject();
        go.transform.position = new Vector3(tx, ty, tz);
        go.transform.rotation = Quaternion.Euler(rx, ry, rz);
        Transform viseur = go.transform;
        RaycastHit hit;
        Vector3 fwd = viseur.TransformDirection(Vector3.forward);
        if (Physics.Raycast(viseur.position, fwd, out hit, 10))
        {
            Aim.Instance.Shoot(playerID, hit);
        }
    }
}
