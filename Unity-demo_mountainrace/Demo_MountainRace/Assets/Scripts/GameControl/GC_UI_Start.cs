using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GC_UI_Start : MonoBehaviour {

	public void PlayGame() {
        SceneManager.LoadScene("001GameScene");
    }


    public void ExitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
