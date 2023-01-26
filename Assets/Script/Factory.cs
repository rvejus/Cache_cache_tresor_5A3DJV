using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public static Factory Instance;
    public List<GameObject> Objects = new List<GameObject>();

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

    public GameObject factoryGO(Vector3 pos, Vector3 rot, Vector3 offset, int playerID, int index)
    {
        GameObject go = Instantiate(Objects[index], pos + offset, Quaternion.Euler(rot));
        go.AddComponent<Treasure>();
        go.GetComponent<Treasure>().playerID = playerID;
        return go;
    }
}
