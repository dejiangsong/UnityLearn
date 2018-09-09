using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlHead : MonoBehaviour {

    public static bool isCanControl = false;      // 能否再控制，直到下一次移动，否则不能改变方向

    private float width;
    private float height;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (isCanControl) {
            // 键盘控制
            if (Input.GetKeyUp(KeyCode.UpArrow)) {
                if (MoveSnake.Direction != Vector2.down) {
                    MoveSnake.Direction = Vector2.up;
                    isCanControl = false;
                }
            } else if (Input.GetKeyUp(KeyCode.DownArrow)) {
                if (MoveSnake.Direction != Vector2.up) {
                    MoveSnake.Direction = Vector2.down;
                    isCanControl = false;
                }
            } else if (Input.GetKeyUp(KeyCode.LeftArrow)) {
                if (MoveSnake.Direction != Vector2.right) {
                    MoveSnake.Direction = Vector2.left;
                    isCanControl = false;
                }
            } else if (Input.GetKeyUp(KeyCode.RightArrow)) {
                if (MoveSnake.Direction != Vector2.left) {
                    MoveSnake.Direction = Vector2.right;
                    isCanControl = false;
                }
            }

            // 触屏控制
            if (Input.touchCount > 0) {
                if (Input.GetTouch(0).phase == TouchPhase.Ended) {
                    Vector3 v = getWidthAndHeight(Input.GetTouch(0).position);
                    if (MoveSnake.Direction == Vector2.up || MoveSnake.Direction == Vector2.down) {
                        if (v.x > transform.position.x) {
                            MoveSnake.Direction = Vector2.right;
                            isCanControl = false;
                        }
                        else {
                            MoveSnake.Direction = Vector2.left;
                            isCanControl = false;
                        }
                    } else if (MoveSnake.Direction == Vector2.left || MoveSnake.Direction == Vector2.right) {
                        if (v.y > transform.position.y) {
                            MoveSnake.Direction = Vector2.up;
                            isCanControl = false;
                        }
                        else {
                            MoveSnake.Direction = Vector2.down;
                            isCanControl = false;
                        }
                    }
                }
            }
        }

    }


    #region 公共函数
    Vector3 getWidthAndHeight(Vector2 v) {
        Vector3 screenToWorldSize = Camera.main.ScreenToWorldPoint(new Vector2(v.x, v.y));
        return screenToWorldSize;
    }
    #endregion
}
