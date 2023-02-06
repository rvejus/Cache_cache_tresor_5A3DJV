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
                Vector3 pos = t.position;
                Quaternion rot = t.rotation;
                Vector3 offset = dec.offset;
                double tx = pos.x;
                double ty = pos.y;
                double tz = pos.z;
                double rx = rot.x;
                double ry = rot.y;
                double rz = rot.z;
                double ox = offset.x;
                double oy = offset.y;
                double oz = offset.z;
                Factory.Instance.factoryGO(tx,ty,tz,rx,ry,rz,ox,oy,oz,PlayerID,dec.index);
                //Factory.Instance.factoryGO(hit.point, dec.rot, dec.offset, PlayerID, dec.index);
                
            }
        }
        else if (gohit.layer == 7)
        {
            if (GameManager.Instance.gamePlays && gohit.GetComponent<Treasure>().playerID != PlayerID)
            {
                GameManager.Instance.winner(PlayerID);
            }
        }
    }
}

