using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class winSceneScript : MonoBehaviour
{
    [SerializeField] private TMP_Text winTxt;
    private void Start()
    {
        int winner = GameManager.Instance.theWinner;
        winTxt.SetText("Player "+winner+" is the winner, Congratulation !");
    }
}
