using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float Damage = 10f;              // 攻击伤害
    public float Speed = 10f;               // 飞行速度
    public float DamageScale = 1f;          // 攻击系数，通过此来调整伤害
    public float SpeedScale = 1f;           // 速度系数，通过此来调整飞行速度
    public bool IsFollow = false;           // 是否跟踪
    [HideInInspector]
    public Transform target;                // 打击目标
    public bool isLaser = false;           // 是否采用激光，飞行速度Speed、是否跟踪IsFollow无意义

    private Rigidbody rb;
    private bool isLooked = false;          // 是否朝向
    private LineRenderer line;              // 激光特效
    private Vector3[] laserPos = new Vector3[2];// 激光位置


    // Use this for initialization
    void Start() {
        if (!isLaser) {                         // 不是激光的处理
            rb = GetComponent<Rigidbody>();
            Destroy(this.gameObject, 10f);      // 时间过长后就销毁
        } else {                    // 是激光的处理
            line = gameObject.GetComponent<LineRenderer>();
        }
    }


    // Update is called once per frame
    void Update() {
        if (!isLaser) {         // 不是激光的处理
            if (target == null)
                Destroy(this.gameObject);
            if (IsFollow)
                followAttack();
            else
                noFollowAttack();
        } else {
            laserAttack();
        }

    }


    #region 攻击方式
    /// <summary>
    /// 跟踪攻击
    /// </summary>
    private void followAttack() {
        transform.LookAt(target);        // 看着目标
        rb.velocity = transform.forward * Speed * SpeedScale;       // 子弹速度设置
    }


    /// <summary>
    /// 不跟踪攻击
    /// </summary>
    private void noFollowAttack() {
        if (!isLooked) {            // 只瞄准一次目标
            transform.LookAt(target);
            isLooked = true;
        }
        rb.velocity = transform.forward * Speed;       // 子弹速度设置
    }


    /// <summary>
    /// 激光攻击
    /// </summary>
    private void laserAttack() {
        if (target != null) {           // 目标还在，继续攻击
            line.SetPositions(laserPos);
            target.GetComponent<Health>().TakeDamage(Damage * DamageScale * Time.deltaTime);
        } else {                        // 目标不在，销毁自身
            Destroy(this.gameObject);
        }
    }
    /// <summary>
    /// 为激光提供位置
    /// </summary>
    /// <param name="pos"></param>
    public void SetLaserPosition(Vector3 headPos) {
        laserPos[0] = headPos;
        laserPos[1] = target.position;
    }
    #endregion


    #region 触发检测
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy") {
            other.GetComponent<Health>().TakeDamage(Damage * DamageScale);
            Destroy(this.gameObject);        // 销毁自身
        }
    }
    #endregion
}
