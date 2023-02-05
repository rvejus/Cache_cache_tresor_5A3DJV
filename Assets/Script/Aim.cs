using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public void Shoot(int PlayerID)
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, out hit, 10))
        {
            GameObject gohit = hit.transform.gameObject;
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
            }else if (gohit.layer == 7)
            {
                if (GameManager.Instance.gamePlays && gohit.GetComponent<Treasure>().playerID!=PlayerID)
                {
                    GameManager.Instance.winner(PlayerID);
                }
            }
        }
    }
}
