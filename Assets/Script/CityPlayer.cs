using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityPlayer : MonoBehaviour
{
    public int playerID;
    public bool isReady;

    public void callShoot()
    {
        Aim.Instance.Shoot(playerID);
    }
    
}
