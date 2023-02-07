using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winNetworkManager : MonoBehaviourPunCallbacks
{
    public void Quit()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        Destroy(GameObject.Find("Manager"));
        SceneManager.LoadScene(0);
    }
}
