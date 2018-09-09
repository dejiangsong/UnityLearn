using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchController : MonoBehaviour {

    public float WitchSpeed = 1f;
    public float WitchMoveXDir = 1f;   // 控制女巫的X轴移动方向

    private float WitchMoveYDir = 1f;   // 控制女巫的Y轴移动方向


    private void Start() {
        InvokeRepeating("changeWithcMoveYDir", 3, 3);       // 每3秒改变一次女巫的Y轴移动方向
    }


    // Update is called once per frame
    void Update() {
        transform.position += new Vector3(Random.Range(0.5f, 1f) * WitchMoveXDir, Random.Range(0.1f, 0.5f) * WitchMoveYDir, transform.position.z) * WitchSpeed * Time.deltaTime;
    }


    /**
     * 控制改变女巫的Y轴移动方向
     */
    void changeWithcMoveYDir() {
        WitchMoveYDir *= -1;
    }
}
