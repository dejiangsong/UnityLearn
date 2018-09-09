using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GC_Game_InitPlayerPrefs : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Find("Car").gameObject.GetComponent<CarController>().Speed = PlayerPrefs.GetFloat("speed");
	}
	
}
