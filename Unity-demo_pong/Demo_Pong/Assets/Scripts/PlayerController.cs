using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 15f;
    public KeyCode upKey = KeyCode.W;       // 按键用
    public KeyCode downKey = KeyCode.S;     // 按键用
    public KeyCode leftKey = KeyCode.A;         // 按键用
    public KeyCode rightKey = KeyCode.D;        // 按键用

    private Rigidbody2D rigidbody2;
    int touchIndex = 0;                         // 触屏用 - 判断并得到是第几个触点
    Vector3 touchPosition;              // 触屏用 - 记录触屏位置
    private float transScale = 0.1f;     // 触屏用 - 位移转速度的比例


    private void Awake() {
        rigidbody2 = GetComponent<Rigidbody2D>();
    }


    void Start() {

    }


    void Update() {
        // 按键控制player
        // 方法一：position
        // 实测，使用改变位置会导致有可能穿透collider，不然，也会在顶到collider时，会反复弹动
        // 且，无rigidbody组件，碰撞到collider也不会阻止其移动
        //if(Input.GetKey(upKey))
        //    gameObject.transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        //else if (Input.GetKey(downKey))
        //    gameObject.transform.position += new Vector3(0, -speed * Time.deltaTime, 0);

        // 方法二：rigidbody
        if (Input.GetKey(upKey)) {
            rigidbody2.velocity = new Vector2(rigidbody2.velocity.x, speed);
        } else if (Input.GetKey(downKey)) {
            rigidbody2.velocity = new Vector2(rigidbody2.velocity.x, -speed);
        } else {
            rigidbody2.velocity = new Vector2(rigidbody2.velocity.x, 0);
        }
        if (Input.GetKey(leftKey)) {
            rigidbody2.velocity = new Vector2(-speed, rigidbody2.velocity.y);
        } else if (Input.GetKey(rightKey)) {
            rigidbody2.velocity = new Vector2(speed, rigidbody2.velocity.y);
        } else {
            rigidbody2.velocity = new Vector2(0, rigidbody2.velocity.y);
        }

        // 触屏控制player
        if (Input.touchCount > 0) {
            for(touchIndex = 0; touchIndex < Input.touchCount; touchIndex++) {
                float x = Input.GetTouch(touchIndex).position.x;    // 得到触点x轴
                string playerName;

                if (x < Screen.width / 2) {     // 得到需要控制的player
                    playerName = "player1";
                } else {
                    playerName = "player2";
                }

                if (Input.GetTouch(touchIndex).phase == TouchPhase.Moved) {     // 触点移动时触发
                    Rigidbody2D PlayerRigidbody2;
                    PlayerRigidbody2 = GameObject.Find(playerName).GetComponent<Rigidbody2D>();
                    // 控制player移动
                    // 方法一：直接使用touch位置，并阻止过界
                    // 注：使用位置可以保证play不飞出屏幕
                    touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(touchIndex).position);
                    if (playerName == "player1" && x < Screen.width / 2)
                        PlayerRigidbody2.transform.position = new Vector3(touchPosition.x, touchPosition.y, transform.position.z);
                    else if (playerName == "player2" && x > Screen.width / 2)
                        PlayerRigidbody2.transform.position = new Vector3(touchPosition.x, touchPosition.y, transform.position.z);
                    // 方法二：使用touch的位置变化量
                    //touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                    //transform.Translate(touchDeltaPosition.x * 0.01f, touchDeltaPosition.y * 0.01f, 0);

                    // 赋予player触点移动速度
                    PlayerRigidbody2.velocity = Input.GetTouch(touchIndex).deltaPosition * transScale;
                }
            }
        }

    }

}
