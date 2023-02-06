using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Factory : MonoBehaviourPunCallbacks
{
    public static Factory Instance;
    public List<GameObject> Objects = new List<GameObject>();
    public Factory photonPlayer;

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

    public void factoryGO(double tx, double ty, double tz, double rx, double ry, double rz, double ox, double oy, double oz, int playerID, int index)
    {
        photonView.RPC("factoryGOpun", RpcTarget.All, tx,ty,tz,rx,ry,rz,ox,oy,oz,playerID,index);
    }

    [PunRPC]
    public void factoryGOpun(double tx, double ty, double tz, double rx, double ry, double rz, double ox, double oy, double oz, int playerID, int index)
    {
        Vector3 pos = new Vector3((float)tx, (float)ty, (float)tz);
        Vector3 rot = new Vector3((float)rx, (float)ry,(float)rz);
        Vector3 offset = new Vector3((float)ox, (float)oy,(float)oz);
        GameObject go = PhotonNetwork.Instantiate(Objects[index].name, pos + offset, Quaternion.Euler(rot));
        go.AddComponent<Treasure>();
        go.GetComponent<Treasure>().playerID = playerID;
        if (GameManager.Instance.playersObjects[playerID] == null)
        {
            GameManager.Instance.playersObjects[playerID] = go;
        }
        else
        {
            //GameObject goToDestroy = GameManager.Instance.playersObjects[PlayerID];
            PhotonNetwork.Destroy(GameManager.Instance.playersObjects[playerID]);
            GameManager.Instance.playersObjects[playerID] = go;
        }
        
    }
}
