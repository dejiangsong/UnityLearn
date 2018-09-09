using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveSnake : MonoBehaviour {

    public GameObject body;                 // body预设体
    public float speed = 0.5f;                  // 移动速度
    public static Vector2 Direction = Vector2.right;   // 移动方向

    private List<Transform> bodys;          // body列表
    private Vector2 beforeMovePosition; // 头部移动之前的位置
    private bool isMove = true;        // 是否移动，如果新增body就不移动


    private void Awake() {
        bodys = new List<Transform>();
    }

    // Use this for initialization
    void Start() {
        InvokeRepeating("Move", 1f, speed);
    }


    private void Move() {
        beforeMovePosition = transform.position;
        transform.Translate(Direction);

        // 核心算法，每次移动都将最后一个body移到head移动之前的位置
        if (!isMove) {  // 如果新增body就移动身体，直接将新增身体移到head移动之前的位置
            GameObject bodyObj = Instantiate(body, beforeMovePosition, Quaternion.identity);   // 不能再接触事件中实例化，否则新增的身体出现在每几秒调用一次Move的头移动之前的位置
            bodys.Insert(0, bodyObj.transform);
            isMove = true;
        } else if (bodys.Count > 0) {   // 无新增body，就将最后一个body移动到head移动之前的位置，再将最后一个body复制到第一个body，最后删除最后一个body
            bodys.Last().position = beforeMovePosition;
            bodys.Insert(0, bodys.Last());
            bodys.RemoveAt(bodys.Count - 1);
        }

        // 判断按键是否能控制
        ControlHead.isCanControl = true;
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "food") {
            GameObject.Find("GameController").SendMessage("AddScore");
            isMove = false;
        } else if (collision.tag == "body" || collision.tag == "wall") {
            GameController.isOver = true;
            GameObject.Find("GameController").SendMessage("PauseGame");
            GameObject.Find("GameController").SendMessage("OverGame");
        }
    }
}
