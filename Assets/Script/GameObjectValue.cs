using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectValue : MonoBehaviour
{
    [SerializeField]
    private float rot_x;
    [SerializeField]
    private float rot_y;
    [SerializeField]
    private float rot_z;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(rot_x, rot_y, rot_z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(rot_x, rot_y, rot_z);
    }
}
