using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeEaten : MonoBehaviour {

    // 碰撞双方（需要其中之一为刚体），都为未设定IsTrigger属性，将采用碰撞检测
    //private void OnCollisionEnter2D(Collision2D collision) {
    //}

    // 接触双方（需要其中之一为刚体），至少有一个设定IsTrigger属性，将采用触发检测
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "head") {
            GameObject.Find("GameController").SendMessage("createFood");
            Destroy(this.gameObject);
        }
    }
}
