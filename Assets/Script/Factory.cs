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

    public void factoryGO(float tx, float ty, float tz, float rx, float ry, float rz, float ox, float oy, float oz, int playerID, int index)
    {
        Vector3 pos = new Vector3(tx, ty, tz);
        Vector3 rot = new Vector3(rx, ry,rz);
        Vector3 offset = new Vector3(ox, oy,oz);
        GameObject go = PhotonNetwork.Instantiate(Objects[index].name, pos + offset, Quaternion.Euler(rot));
        go.AddComponent<Treasure>();
        go.GetComponent<Treasure>().playerID = playerID;
        go.GetComponent<AudioSource>().Play();
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

    [PunRPC]
    public void factoryGOpun(float tx, float ty, float tz, float rx, float ry, float rz, float ox, float oy, float oz, int playerID, int index)
    {
        
    }
}
