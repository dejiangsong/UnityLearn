using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float Speed = 5f;

    private Vector3 oldPosition;
    private Vector3 movePosition;
    private bool isClickDown;


    // Use this for initialization
    void Start() {

    }


    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {  // 按下按键
            isClickDown = true;
            oldPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0)) {    // 释放按键
            isClickDown = false;
            oldPosition = Input.mousePosition;
        }

        if (isClickDown) {      // 移动
            movePosition = (Input.mousePosition - oldPosition) * -1;
            movePosition.z = movePosition.y;
            movePosition.y = 0;
            oldPosition = Input.mousePosition;
            transform.Translate(movePosition * Time.deltaTime * Speed, Space.World);
        }

        float sw = Input.GetAxis("Mouse ScrollWheel");
        transform.position += transform.forward * Time.deltaTime * sw * Speed * 100f;
    }
}
