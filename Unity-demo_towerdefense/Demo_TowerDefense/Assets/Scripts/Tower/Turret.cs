using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public GameObject BulletPrefab;
    public float AttackRate = 1f;                   // 攻击间隔
    public float AttackRateScale = 1f;                   // 攻击间隔系数
    public float AttackPowerScale = 1f;             // 攻击力系数，用来为升级时做调整
    public float SpeedScale = 1f;                   // 速度系数，用来为升级时做调整
    public bool isLaser = false;       // 是否采用激光攻击，攻击间隔AttackRate无意义

    private List<GameObject> enemys = new List<GameObject>();       // 敌人集合
    private float attackTimer = 0f;     // 攻击间隔计时器
    private Transform head;             // 头的位置
    private GameObject laser;           // 激光物体
    private float oldAttackPowerScale;   // 旧的攻击系数，为激光提高攻击判断专用


    private void Start() {
        head = transform.Find("Head").GetComponent<Transform>();    // 得到头的位置
    }


    private void Update() {
        clearEmpty();        // 清除空的敌人集合

        // 进行攻击
        if (!isLaser) {     // 采用炮弹攻击时
            attackTimer += Time.deltaTime;
            if (enemys.Count > 0 && attackTimer >= AttackRate * AttackRateScale) {     // 攻击条件：有敌人 + 达到攻击间隔
                attackTimer = 0;
                attack();       // 攻击
            }
        } else {            // 采用激光攻击时
            if (enemys.Count > 0) {     // 攻击条件：有敌人
                laserAttack();      // 激光攻击
            } else {
                Destroy(laser);     // 没有敌人，销毁激光
            }
        }


        // 改变头的朝向
        if (enemys.Count > 0) {
            Vector3 target = enemys[0].transform.position;
            target.y = head.position.y;
            head.LookAt(target);
        }
    }


    #region 攻击的函数
    /// <summary>
    /// 攻击
    /// </summary>
    private void attack() {
        Vector3 bulletSpawn = transform.Find("Head").Find("BulletSpawn").transform.position;
        GameObject bullet = GameObject.Instantiate(BulletPrefab, bulletSpawn, Quaternion.identity);
        bullet.GetComponent<Bullet>().target = enemys[0].transform;     // 设置攻击目标
        bullet.GetComponent<Bullet>().DamageScale = AttackPowerScale;       // 设置子弹攻击系数
        bullet.GetComponent<Bullet>().SpeedScale = SpeedScale;       // 设置子弹飞行系数
    }


    /// <summary>
    /// 从集合中清除空的敌人
    /// </summary>
    private void clearEmpty() {
        while (enemys.Count > 0 && enemys[0] == null) {     // 循环判断从第一个敌人开始是否为空，处理是否敌人在一些情况下被销毁了，导致不是由TriggerExit移出而找不到
            enemys.RemoveAt(0);
        }
        if (enemys.Count <= 0) return;      // 没有敌人，则直接放弃攻击

    }


    private void laserAttack() {
        Vector3 bulletSpawn = transform.Find("Head").Find("BulletSpawn").transform.position;
        if (laser == null) {      // 没有激光就实例化一个激光
            laser = GameObject.Instantiate(BulletPrefab, bulletSpawn, Quaternion.identity);
            oldAttackPowerScale = 1f;   // 新的激光，需要重新设置攻击系数，置为默认的1f可以使下面产生重新设置
        }
        Bullet bullet = laser.GetComponent<Bullet>();
        bullet.target = enemys[0].transform;     // 设置设置激光的目标位置
        bullet.SetLaserPosition(bulletSpawn);     // 设置激光的头位置
        if (AttackPowerScale - oldAttackPowerScale != 0f) {
            bullet.DamageScale = AttackPowerScale;       // 设置激光攻击系数
            oldAttackPowerScale = AttackPowerScale;     // 置为旧的攻击系数
        }
    }
    #endregion


    #region 触发器事件
    /// <summary>
    /// 有敌人接近触发器后，添加到敌人列表
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy")
            enemys.Add(other.gameObject);
    }


    /// <summary>
    /// 敌人离开后，移出敌人列表
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other) {
        if (other.tag == "Enemy")
            enemys.Remove(other.gameObject);
    }
    #endregion


    /// <summary>
    /// 被销毁时的处理
    /// </summary>
    private void OnDestroy() {
        if (laser != null)
            Destroy(laser);
    }
}
