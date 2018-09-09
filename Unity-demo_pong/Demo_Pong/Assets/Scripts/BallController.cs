using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    private Rigidbody2D rigidbody2;


    void Start() {
        rigidbody2 = GetComponent<Rigidbody2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "player") {
            if(collision.rigidbody.velocity.y != 0) {   //如果板没速度，就不改变球的速度
                Vector2 v = rigidbody2.velocity;
                v.y = v.y / 3 + collision.rigidbody.velocity.y / 3 * 2;
                rigidbody2.velocity = v;
            }
        }else if(collision.collider.name == "leftWall" || collision.collider.name == "rightWall") {
            GameController.Instance.ChangeScore(collision.collider.name);
        }
    }


}
