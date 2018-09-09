using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GC_UI_Game_Level1 : MonoBehaviour {

    public Text PauseButtonText;

    private bool isPause = false;
    

	public void PauseGame() {
        isPause = !isPause;
        if (isPause) {
            Time.timeScale = 0f;
            changePauseButtonText("I>");
        } else {
            Time.timeScale = 1f;
            changePauseButtonText("||");
        }
    }


    public void OnceMoreGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void BackStartView() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("000StartScene");
    }


    #region 公共函数
    private void changePauseButtonText(string text) {
        PauseButtonText.text = text;
    }
    #endregion
}
