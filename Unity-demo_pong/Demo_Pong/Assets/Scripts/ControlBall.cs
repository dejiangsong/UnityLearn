using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBall : MonoBehaviour {

    public GameObject ball;
    public float Force = 70f;
    public bool IsHoldSpeed = true;
    public float HoldSpeedValue = 40f;

    private Rigidbody2D ballRigidbody2;
    private Vector3 initPosition;
    private Vector2 ballV;

    private void Awake() {
        initPosition = ball.transform.position;
        ballRigidbody2 = ball.GetComponent<Rigidbody2D>();
    }


    void Start() {
        resetBall();
    }


    private void Update() {
        if (IsHoldSpeed) {
            ballV = ballRigidbody2.velocity;
            if (Mathf.Sqrt(Mathf.Pow(ballV.x, 2) + Mathf.Pow(ballV.y, 2)) > HoldSpeedValue) {
                float xyScaleAngel = ballV.x != 0 ? Mathf.Asin(ballV.y / ballV.x) : 0;
                print(xyScaleAngel);
                int xDir = ballV.x > 0 ? 1 : -1;
                int yDir = ballV.y > 0 ? 1 : -1;
                ballRigidbody2.velocity = new Vector2(xDir * HoldSpeedValue * Mathf.Cos(xyScaleAngel), yDir * HoldSpeedValue * Mathf.Sin(xyScaleAngel));
            }
        }
    }


    void resetBall() {
        ball.transform.position = initPosition;
        ballRigidbody2.velocity = Vector2.zero;

        int direction = Random.Range(0, 2);
        if (direction == 0) {
            ballRigidbody2.AddForce(new Vector2(Force, 0));
        } else {
            ballRigidbody2.AddForce(new Vector2(-Force, 0));
        }
    }
}
