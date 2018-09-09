using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GC_UI_Start_SpeedChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetFloat("speed", 60);
	}
	
	
    public void changeSpeedToggle1(bool isChange) {
        PlayerPrefs.SetFloat("speed", 60);
    }

    public void changeSpeedToggle2(bool isChange) {
        PlayerPrefs.SetFloat("speed", 250);
    }

    public void changeSpeedToggle3(bool isChange) {
        PlayerPrefs.SetFloat("speed", 400);
    }
}
