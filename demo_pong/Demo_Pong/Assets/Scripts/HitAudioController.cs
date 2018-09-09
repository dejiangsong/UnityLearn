using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAudioController : MonoBehaviour {

    public bool IsChangePitch = false;

    private AudioSource audio;

    void Awake() {
        audio = GetComponent<AudioSource>();
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.name == "ball") {
            if(IsChangePitch)
                audio.pitch = Random.Range(0.8f, 1.2f);
            audio.Play();
        }
    }
}
