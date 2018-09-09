using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerStatus {
    public bool isOnGround;
    public bool isWalking;
    public bool isDown;
    public bool isLookUp;
    public bool isLookRight;
    public bool isShooting;
}

public class PlayerController : MonoBehaviour {

    public float XSpeed = 1f;       // X轴速度
    public float YForce = 1f;       // Y轴的跳跃力
    public GameObject Bullet;       // 子弹的预设体
    public float ShootInterval = 0.3f;   // 开枪间隔

    public PlayerStatus status;     // 角色行为状态

    private Rigidbody2D rb;
    private float h;
    private int groundLayerMask;
    private float shootTimer;
    private bool canShoot;


    private void Awake() {
        rb = this.GetComponent<Rigidbody2D>();
        groundLayerMask = LayerMask.GetMask("Ground");
        status.isLookRight = true;      // 默认是false，但一般角色是面向右出场的
    }


    // Use this for initialization
    void Start() {

    }


    // Update is called once per frame
    void Update() {
        decideStatus();     // 判断各个状态
        move();
        jump();
        shoot();
    }


    private void move() {
        float h = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.S))    // 如果是蹲下状态，速度减至1/3
            rb.velocity = new Vector2(h * XSpeed / 3, rb.velocity.y);
        else
            rb.velocity = new Vector2(h * XSpeed, rb.velocity.y);
    }


    private void jump() {
        if (status.isOnGround) {
            if (Input.GetKeyDown(KeyCode.K)) {
                rb.velocity = new Vector2(rb.velocity.x, 0);        // 跳跃时，移出Y方向速度，避免在高地边角跳跃时，容易导致力的速度累加
                rb.AddForce(new Vector2(0, YForce));
            }
        }
    }


    /**
     * 进行射击
     * */
    private void shoot() {
        if (!canShoot)      // 只有不能开枪时，才计时
            shootTimer += Time.deltaTime;
        if (shootTimer > ShootInterval) {    // 达到攻击间隔
            shootTimer -= ShootInterval;
            canShoot = true;    // 达到计时，允许开枪
        }

        if (Input.GetKey(KeyCode.J) && canShoot) {
            if (status.isLookUp && status.isLookRight)         // 朝上
                fire(new Vector3(transform.position.x + 0.2f, transform.position.y + 1f, 0), 90);
            else if (status.isLookUp && !status.isLookRight)
                fire(new Vector3(transform.position.x - 0.2f, transform.position.y + 1f, 0), 90);
            else if (status.isOnGround && !status.isDown && status.isLookRight)  // 在地上+不蹲下
                fire(new Vector3(transform.position.x + 0.66f, transform.position.y + 0.54f, 0), 0);
            else if (status.isOnGround && !status.isDown && !status.isLookRight)
                fire(new Vector3(transform.position.x - 0.66f, transform.position.y + 0.54f, 0), 180);
            else if (status.isOnGround && status.isDown && status.isLookRight)  // 在地上 + 蹲下
                fire(new Vector3(transform.position.x + 0.66f, transform.position.y + 0.3f, 0), 0);
            else if (status.isOnGround && status.isDown && !status.isLookRight)
                fire(new Vector3(transform.position.x - 0.66f, transform.position.y + 0.3f, 0), 180);
            else if (!status.isOnGround && status.isLookRight && !status.isDown)      // 在空中 + 朝右
                fire(new Vector3(transform.position.x + 0.66f, transform.position.y + 0.54f, 0), 0);
            else if (!status.isOnGround && !status.isLookRight && !status.isDown)
                fire(new Vector3(transform.position.x - 0.66f, transform.position.y + 0.54f, 0), 180);
            else if (!status.isOnGround && status.isDown && status.isLookRight)       // 空中 + 朝下
                fire(new Vector3(transform.position.x + 0.2f, transform.position.y - 0.2f, 0), -90);
            else if (!status.isOnGround && status.isDown && !status.isLookRight)
                fire(new Vector3(transform.position.x - 0.2f, transform.position.y - 0.2f, 0), -90);

            canShoot = false;   // 重置能否开枪标志
        }
    }


    /**
     * 开火
     * 射出子弹bullet
     * */
    private void fire(Vector3 position, float rotation) {
        GameObject.Instantiate(Bullet, position, Quaternion.Euler(0, 0, rotation));
    }


    #region util
    /**
     * 进行多个状态判断
     */
    private void decideStatus() {
        status.isOnGround = decideIsOnGround();
        status.isWalking = decideIsWalking();
        status.isDown = decideIsDown();
        status.isLookUp = decideIsLookUp();
        status.isLookRight = decideIsLookRight();
        status.isShooting = decideIsShoot();
    }


    /**
     * 判断角色是否在地面
     * 注：因为碰撞的两物体是不会直接贴合的，所以不需要括号内的操作(需要从角色下边的碰撞边往上移动一点判断，否则直接撞上了，不好检测)
     *          需要从角色左右边界都检测一下，避免卡在地面边界，实际上是站在地上，确无法实现跳跃
     *          动画状态控制判断地面时，再加上是否Y轴速度小于一定值，以免动画在跳过高一些的地面时，会发生idle和jump动画的闪动
     */
    private bool decideIsOnGround() {
        bool result = Physics2D.Raycast(transform.position, Vector3.down, 0.2f, groundLayerMask)
            || Physics2D.Raycast(transform.position + Vector3.left * 0.15f, Vector3.down, 0.2f, groundLayerMask)
            || Physics2D.Raycast(transform.position + Vector3.right * 0.15f, Vector3.down, 0.2f, groundLayerMask)
            && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) < 0.1f;
        return result;
    }


    /**
     * 判断是否在走动
     */
    private bool decideIsWalking() {
        bool result = false;
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
            result = true;
        else
            result = false;
        return result;
    }


    /**
     * 判断是否蹲下
     */
    private bool decideIsDown() {
        bool result = false;
        if (Input.GetKey(KeyCode.S))
            result = true;
        else
            result = false;
        return result;
    }


    /**
     * 判断是否朝上
     */
    private bool decideIsLookUp() {
        bool result = false;
        if (Input.GetKey(KeyCode.W))
            result = true;
        else
            result = false;
        return result;
    }


    /**
     * 判断是否向右
     */
    private bool decideIsLookRight() {
        bool result = status.isLookRight;
        if (Input.GetAxis("Horizontal") > 0)
            result = true;
        else if (Input.GetAxis("Horizontal") < 0)
            result = false;
        return result;
    }


    /**
     * 判断是否开枪
     */
    private bool decideIsShoot() {
        bool result = false;
        if (Input.GetKey(KeyCode.J))
            result = true;
        else
            result = false;
        return result;
    }
    #endregion
}
