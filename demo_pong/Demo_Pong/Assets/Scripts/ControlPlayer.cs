using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;


    private void Awake() {
        resetPlayer();
    }

    // Use this for initialization
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }


    void resetPlayer() {
        Vector3 screenToWorldSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));   //将屏幕的右上点以世界坐标原点转换为世界坐标中的点
        float width = screenToWorldSize.x;
        float height = screenToWorldSize.y;
        
        player1.transform.position = new Vector3(-width + 1, 0, 0);
        player2.transform.position = new Vector3(width - 1, 0, 0);
    }
}
