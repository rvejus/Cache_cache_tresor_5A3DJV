using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Rendering;

public class Aim : MonoBehaviourPunCallbacks
{
    public static Aim Instance;
    private void Awake()
    {
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy (gameObject);
            }

        }
    }
    
    public void Shoot(int PlayerID, RaycastHit hit)
    {
        GameObject gohit = hit.transform.gameObject;
        Debug.Log(PlayerID);
        if (gohit.layer == 6)
        {
            if (!GameManager.Instance.gamePlays)
            {
                Decor dec = gohit.GetComponent<Decor>();
                Transform t = hit.transform;
                Vector3 pts = hit.point;
                Vector3 rot = dec.rot;
                Vector3 offset = dec.offset;
                float tx = pts.x;
                float ty = pts.y;
                float tz = pts.z;
                float rx = rot.x;
                float ry = rot.y;
                float rz = rot.z;
                float ox = offset.x;
                float oy = offset.y;
                float oz = offset.z;
                Factory.Instance.factoryGO(tx,ty,tz,rx,ry,rz,ox,oy,oz,PlayerID,dec.index);
                //Factory.Instance.factoryGO(hit.point, dec.rot, dec.offset, PlayerID, dec.index);
                
            }
        }
        else if (gohit.layer == 7)
        {
            if (GameManager.Instance.gamePlays && !gohit.gameObject.GetPhotonView().IsMine)
            {
                GameManager.Instance.photonView.RPC("setWinner", RpcTarget.AllBuffered, PlayerID);
            }
        }
    }
}

