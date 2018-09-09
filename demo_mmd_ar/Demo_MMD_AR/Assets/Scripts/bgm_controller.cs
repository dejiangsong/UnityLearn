using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgm_controller : MonoBehaviour {

    public GameObject bgm_start;
    public GameObject bgm_end;
    private Vector3 v;

    // Use this for initialization
    void Start()
    {
        v = new Vector3(0, 0.01f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 1)
            transform.position += v;
        if (Time.time >= 7)
            bgm_end.SetActive(true);
    }
}
