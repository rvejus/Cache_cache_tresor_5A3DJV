using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDecorSpawn : MonoBehaviour
{

    public List<GameObject> objectsDecor = new List<GameObject>();
    [SerializeField] private float activationChance = 0.5f;
    void Start()
    {
        foreach (GameObject obj in objectsDecor)
        {
            if (Random.Range(0f, 1f) < activationChance)
            {
                obj.SetActive(true);
            }
            else
            {
                obj.SetActive(false);
            }
        }
    }


}
