using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float XSpeed = 1f;
    public float YSpeed = 1f;
    public int MaxJumpNum = 1;      // 允许的最大跳跃次数

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 velocity;       // 速度
    private int jumpCount;        // 跳跃次数计数
    private float xForce;           // X轴移动的力的大小
    private float yForce;           // Y轴移动的力的大小
    private bool isGround;      // 是否在地面
    private bool isWall;            // 是否在墙边
    private bool isOnRightOfWall;   // 是否在墙右边
    private bool isKeyToWall;       // 是否贴墙(在墙边，按靠墙的方向键)

    private void Awake() {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
    }


    // Use this for initialization
    void Start() {
        xForce = XSpeed * 70f;      // 将X速度转换为需要的X对应力
        yForce = YSpeed * 3000f;      // 将Y速度转换为需要的Y对应力
    }

    // Update is called once per frame
    void Update() {
        if (keyMove() != 0 || Input.GetKeyDown(KeyCode.Space))  // 按键移动条件：有横向移动按键 + 按空白键
            move(keyMove());
        setAnimation();
    }


    /**
     * 按键移动
     */
    private float keyMove() {
        float xMove = Input.GetAxis("Horizontal");
        return xMove;
    }


    /**
     * 控制角色移动
     */
    private void move(float xMove) {
        // 前后移动
        if (xMove > 0) {     // 右走
            rb.AddForce(Vector2.right * xForce);
        } else if (xMove < 0) { // 左走
            rb.AddForce(Vector2.left * xForce);
        } else {    // 没有按键，将速度置为0
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // 修改朝向
        if (xMove > 0.1f && !isWall) {
            transform.localScale = new Vector3(1, 1, 1);
        } else if (xMove < -0.1f && !isWall) {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // 靠墙滑行
        isKeyToWall = false;    // 先取消贴墙的判断值
        if (isWall && !isGround) {  // 条件：靠墙 + 不站地面 + 一直按住往墙上的方向键
            // 是否在左墙按左键，右墙按右键
            if ((isOnRightOfWall && xMove < 0) || (!isOnRightOfWall && xMove > 0)) {
                isKeyToWall = true;
            } else {
                isKeyToWall = false;
            }

            // 靠墙减速为零滑行
            if (isKeyToWall)
                rb.velocity = Vector2.zero;     // 将速度置为零，靠摩擦力下滑

            // 调整朝向墙
            if (!isOnRightOfWall && isKeyToWall)    // 条件：在墙的左/右 + 是否贴墙
                transform.localScale = new Vector3(-1, 1, 1);
            else if (isOnRightOfWall && isKeyToWall)
                transform.localScale = new Vector3(1, 1, 1);
        }

        // 跳跃
        // 注：测试后，发现贴墙时，跳会受到墙体各方面的影响，所以贴墙跳时，先将其移开墙面
        if (jumpCount > 0 && Input.GetKeyDown(KeyCode.Space)) {  // 是否在能跳的次数，已经设置为第一次跳了，其实可以不必判断
            if (jumpCount == MaxJumpNum && isGround || (isWall && isKeyToWall)) {   //第一段跳跃条件：跳跃计数为最大值 + 站地上 || 贴墙
                rb.AddForce(Vector2.up * yForce);
                if (isKeyToWall) {  // 如果贴墙，还需受横向力弹开墙体
                    transform.position = new Vector3(transform.position.x + (isOnRightOfWall ? 0.1f : -0.1f), transform.position.y, transform.position.z);    // 贴墙跳避免受墙体影响，先将其移开墙体
                    rb.AddForce(Vector2.up * yForce / 4);
                    rb.AddForce(Vector2.right * (isOnRightOfWall ? 1 : -1) * yForce / 2);
                }
            } else {    // 多段跳
                // 赋予Y轴力
                rb.velocity = Vector2.zero;     // 移除当时的速度，避免速度和力的累加
                rb.AddForce(Vector2.up * yForce);
            }

            jumpCount--;
        }
        // 是否是直接落下悬空的，通过是否按跳跃键判断
        if (!isGround && !isKeyToWall && Input.GetKeyDown(KeyCode.Space))    // 条件：不在地上 + 不贴墙 + 按下跳跃键
            jumpCount--;

    }


    /**
     * 设置动画
     */
    private void setAnimation() {
        velocity = rb.velocity;
        anim.SetFloat("XSpeed", Mathf.Abs(velocity.x));     // 设置跑动动画
        anim.SetFloat("YSpeed", velocity.y);     // 设置跳动动画

        anim.SetBool("IsWall", isWall); anim.SetBool("IsGround", isGround);
        anim.SetBool("IsKeyToWall", isKeyToWall);
    }


    #region 碰撞检测
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Ground")
            isGround = true;
        if (collision.collider.tag == "Wall") {
            isWall = true;

            // 判断人与墙的位置关系
            if (transform.position.x - collision.collider.gameObject.transform.position.x < 0)
                isOnRightOfWall = false;
            else if (transform.position.x - collision.collider.gameObject.transform.position.x > 0)
                isOnRightOfWall = true;
        }

        // 碰到地面和墙后，恢复最大跳跃次数
        if (collision.collider.tag == "Ground" || collision.collider.tag == "Wall")
            jumpCount = MaxJumpNum;
    }


    /**
     * 碰撞持续的检测，避免isGround碰撞后，再碰撞isWall后，isGround被莫名其妙除去
     */
    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.collider.tag == "Ground")
            isGround = true;
        if (collision.collider.tag == "Wall")
            isWall = true;
    }


    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.collider.tag == "Ground")
            isGround = false;
        if (collision.collider.tag == "Wall")
            isWall = false;
    }
    #endregion

}
