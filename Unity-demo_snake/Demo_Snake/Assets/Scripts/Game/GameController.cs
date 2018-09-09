using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static bool isOver = false;
    public GameObject overPanel;
    public static int Score = 0;
    

    public void OverGame() {
        overPanel.SetActive(true);
    }


    public void AddScore() {
        GameController.Score++;
        GameObject.Find("GameController").SendMessage("changeScoreText");
    }

}
