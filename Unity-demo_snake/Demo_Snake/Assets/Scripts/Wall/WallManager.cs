using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour {

    private BoxCollider2D upWall;
    private BoxCollider2D downWall;
    private BoxCollider2D leftWall;
    private BoxCollider2D rightWall;

    private float width;
    private float height;

    private void Awake() {
        resetWall();
    }


    void resetWall() {
        upWall = transform.Find("upWall").GetComponent<BoxCollider2D>();
        downWall = transform.Find("downWall").GetComponent<BoxCollider2D>();
        leftWall = transform.Find("leftWall").GetComponent<BoxCollider2D>();
        rightWall = transform.Find("rightWall").GetComponent<BoxCollider2D>();

        Vector3 screenToWorldSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));   //将屏幕的右上点以世界坐标原点转换为世界坐标中的点
        width = screenToWorldSize.x;
        height = screenToWorldSize.y;
        // 注意，屏幕的中心点为坐标原点
        float adjustValue = 3.2f;
        upWall.transform.position = new Vector3(0, height + 0.5f / adjustValue, 0);
        downWall.transform.position = new Vector3(0, -height - 0.5f / adjustValue, 0);
        leftWall.transform.position = new Vector3(-width - 0.5f / adjustValue, 0, 0);
        rightWall.transform.position = new Vector3(width + 0.5f / adjustValue, 0, 0);

        upWall.size = downWall.size = new Vector2(width * 2 * adjustValue + 2f * adjustValue, 1);    //+2补边角
        leftWall.size = rightWall.size = new Vector2(1, height * 2 * adjustValue + 2f * adjustValue);

    }


    #region 公共函数
    void getWidthAndHeight() {
        Vector3 screenToWorldSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        width = screenToWorldSize.x;
        height = screenToWorldSize.y;
    }
    #endregion
}
