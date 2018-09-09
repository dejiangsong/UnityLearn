using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCar : MonoBehaviour {

    private Transform car;
    private Vector3 screenSize;

    private void Awake() {
        car = GameObject.FindGameObjectWithTag("Car").gameObject.transform;
    }


    // Use this for initialization
    void Start() {
        screenToWorldPosition();
    }

    // Update is called once per frame
    void Update() {
        transform.position = new Vector3(car.position.x, car.position.y + screenSize.y/2, transform.position.z);
    }


    #region 公共函数
    private void screenToWorldPosition() {
        screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
    #endregion
}
