using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : MonoBehaviour {

    public float Speed = 5f;        // 飞行速度
    public float DestroyDistance = 5f;      // 销毁距离
    public float DestroyTime = 5f;      // 销毁时长

    private Vector3 oldPos;


    // Use this for initialization
    void Start() {
        oldPos = transform.position;
        GameObject.Destroy(this.gameObject, DestroyTime);       // 过一定时间后销毁子弹

        transform.Find("SparkStart").gameObject.SetActive(true);        // 显示火焰
        // 过0.5秒后无效火焰
        Invoke("knockDownSpark", 0.1f);

        this.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(transform.eulerAngles.z * Mathf.PI / 180), Mathf.Sin(transform.eulerAngles.z * Mathf.PI / 180)) * Speed;
    }


    // Update is called once per frame
    void Update() {
        transform.Find("SparkStart").gameObject.transform.position = oldPos;
        if (Mathf.Abs(transform.position.x - oldPos.x) > DestroyDistance || Mathf.Abs(transform.position.y - oldPos.y) > DestroyDistance)   // 超过距离销毁
            GameObject.Destroy(this.gameObject);
    }


    /**
     * 消除火花
     * */
    private void knockDownSpark() {
        transform.Find("SparkStart").gameObject.SetActive(false);
    }


    /**
     * 碰撞到物体
     * */
    private void OnCollisionEnter2D(Collision2D collision) {
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;       // 撞到物体后不再移动
        transform.Find("Bullet").gameObject.SetActive(false);
        transform.Find("SparkEnd").gameObject.SetActive(true);
        GameObject.Destroy(this.gameObject, 0.1f);     // 撞到物体后，显示火焰，0.5秒后销毁
    }

}
