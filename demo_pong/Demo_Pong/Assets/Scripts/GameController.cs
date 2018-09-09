using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private static GameController _instance;
    public static GameController Instance {
        get {
            return _instance;
        }
    }

    public Text Text1;
    public Text Text2;

    private int score1;
    private int score2;
    private bool isPause = false;


    private void Awake() {
        _instance = this;
    }


    // Use this for initialization
    void Start() {

    }


    // Update is called once per frame
    void Update() {

    }


    public void ChangeScore(string wallName) {
        if(wallName == "leftWall") {
            score2++;
        }
        else if(wallName== "rightWall" ) {
            score1++;
        }
        showScore();

        //gameObject.SendMessage("resetBall");  //每一次加分是否重置求的位置
    }


    public void PauseGame() {
        isPause = !isPause;
        if (isPause) {
            Time.timeScale = 0;
            GameObject.Find("pauseButton").transform.Find("Text").GetComponent<Text>().text = "I>";
        } else {
            Time.timeScale = 1;
            GameObject.Find("pauseButton").transform.Find("Text").GetComponent<Text>().text = "II";
        }
    }


    public void ResetGame() {
        score1 = score2 = 0;
        showScore();
        GameObject.Find("GameController").SendMessage("resetBall");
    }


    public void backStartView() {
        isPause = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("001StartScene");
    }


    public void ResetBall() {
        GameObject.Find("GameController").SendMessage("resetBall");
    }


    #region 公共函数
    /**
     * 显示分数
     */
     private void showScore() {
        Text1.text = score1.ToString();
        Text2.text = score2.ToString();
    }
    #endregion
}
