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
        double fx = fwd.x;
        double fy = fwd.y;
        double fz = fwd.z;
        double tx = pos.x;
        double ty = pos.y;
        double tz = pos.z;
        photonView.RPC("CallShoot", RpcTarget.All, playerID,fx,fy,fz,tx,ty,tz);
        
    }
    
    [PunRPC]
    public void CallShoot(int IDPlayer, double fx, double fy, double fz, double tx, double ty, double tz)
    {
        Vector3 pos = new Vector3((float)tx, (float)ty, (float)tz);
        RaycastHit hit;
        Vector3 fwd = new Vector3((float)fx, (float)fy, (float)fz);
        if (Physics.Raycast(pos, fwd, out hit, 10))
        {
            Aim.Instance.Shoot(playerID, hit);
        }
    }

    [PunRPC]
    public void SetReady()
    {
        isReady = true;
    }
}
