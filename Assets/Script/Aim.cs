using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
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
                GameObject go = Factory.Instance.factoryGO(hit.point, dec.rot, dec.offset, PlayerID, dec.index);
                go.transform.parent = gohit.transform;
                if (GameManager.Instance.playersObjects[PlayerID] == null)
                {
                    GameManager.Instance.playersObjects[PlayerID] = go;
                }
                else
                {
                    //GameObject goToDestroy = GameManager.Instance.playersObjects[PlayerID];
                    Destroy(GameManager.Instance.playersObjects[PlayerID]);
                    GameManager.Instance.playersObjects[PlayerID] = go;
                }
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

