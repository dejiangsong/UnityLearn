using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour {

    private BoxCollider2D upWall;
    private BoxCollider2D downWall;
    private BoxCollider2D leftWall;
    private BoxCollider2D rightWall;


    // Use this for initialization
    void Start() {
        resetWall();
    }

    // Update is called once per frame
    void Update() {

    }


    void resetWall() {
        upWall = transform.Find("upWall").GetComponent<BoxCollider2D>();
        downWall = transform.Find("downWall").GetComponent<BoxCollider2D>();
        leftWall = transform.Find("leftWall").GetComponent<BoxCollider2D>();
        rightWall = transform.Find("rightWall").GetComponent<BoxCollider2D>();

        Vector3 screenToWorldSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));   //将屏幕的右上点以世界坐标原点转换为世界坐标中的点
        float width = screenToWorldSize.x;
        float height = screenToWorldSize.y;

        // 注意，屏幕的中心点为坐标原点
        upWall.transform.position = new Vector3(0, height + 0.5f, 0);
        downWall.transform.position = new Vector3(0, -height - 0.5f, 0);
        leftWall.transform.position = new Vector3(-width - 0.5f, 0, 0);
        rightWall.transform.position = new Vector3(width + 0.5f, 0, 0);

        upWall.size = downWall.size = new Vector2(width * 2 + 2, 1);    //+2补边角
        leftWall.size = rightWall.size = new Vector2(1, height * 2);

    }
}
