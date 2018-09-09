using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour {

    private AudioSource audio;
    
	void Awake () {
        audio = GetComponent<AudioSource>();
	}


    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.name == "ball") {
            audio.pitch = Random.Range(0.8f, 1.2f);
            audio.Play();
        }
    }
    
}
