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
        Vector3 pos = viseur.position;
        Quaternion rot = viseur.rotation;
        Vector3 fwd = viseur.TransformDirection(Vector3.forward);
        float fx = fwd.x;
        float fy = fwd.y;
        float fz = fwd.z;
        float tx = pos.x;
        float ty = pos.y;
        float tz = pos.z;
        photonView.RPC("CallShoot", RpcTarget.All, playerID,fx,fy,fz,tx,ty,tz);
        
    }
    
    [PunRPC]
    public void CallShoot(int IDPlayer, float fx, float fy, float fz, float tx, float ty, float tz)
    {
        Vector3 pos = new Vector3(tx, ty, tz);
        RaycastHit hit;
        Vector3 fwd = new Vector3(fx, fy, fz);
        if (Physics.Raycast(pos, fwd, out hit, 10))
        {
            Aim.Instance.Shoot(playerID, hit);
        }
    }
}
