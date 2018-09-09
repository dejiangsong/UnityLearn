using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIControl : MonoBehaviour {

    public Text PauseText;
    public Text ScoreText;

    private bool isPause = false;


    public void PauseGame() {
        isPause = !isPause;
        if (isPause) {
            Time.timeScale = 0f;
            PauseText.text = "|>继续";
        } else {
            Time.timeScale = 1f;
            PauseText.text = "II暂停";
        }
    }


    private void ContinueGame() {
        Time.timeScale = 1f;
    }

    
    public void OnceMoreGame() {
        ContinueGame();
        SceneManager.LoadScene("001GameScene");
    }


    public void BackStartScene() {
        ContinueGame();
        SceneManager.LoadScene("000StartScene");
    }


    private void changeScoreText() {
        ScoreText.text = GameController.Score.ToString();
    }
}
