using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimStatusController : MonoBehaviour {

    public PlayerAnimationController[] PlayerAnim;        // 获得角色动画播放脚本

    private PlayerController pc;        // 获得角色另外一个脚本：角色控制脚本，用以获取角色的状态
    private PlayerAnimStatus status;

    private enum PlayerAnimStatus {
        Idle,
        Walk,
        Jump,
        Down,
        DownWalk,
        IdleShoot,
        IdleUpShoot,
        WalkShoot,
        WalkUpShoot,
        JumpShoot,
        JumpUpShoot,
        JumpDownShoot,
        DownShoot,
        DownWalkShoot
    }


    private void Awake() {
        pc = GetComponent<PlayerController>();
        status = PlayerAnimStatus.Idle;
    }


    // Use this for initialization
    void Start() {
    }


    // Update is called once per frame
    void Update() {
        changeStatus();     // 改变状态
        playAnim();         // 根据状态播放相应动画
        changePlayerOrientation();      // 改变朝向
    }


    #region util
    /**
     * 改变状态
     */
    private void changeStatus() {
        PlayerStatus ps = pc.status;
        if (ps.isOnGround && !ps.isWalking && !ps.isDown && !ps.isLookUp && !ps.isShooting)
            status = PlayerAnimStatus.Idle;
        else if (ps.isOnGround && ps.isWalking && !ps.isDown && !ps.isLookUp && !ps.isShooting)
            status = PlayerAnimStatus.Walk;
        else if (!ps.isOnGround && !ps.isDown && !ps.isLookUp && !ps.isShooting)
            status = PlayerAnimStatus.Jump;
        else if (ps.isOnGround && !ps.isWalking && ps.isDown && !ps.isLookUp && !ps.isShooting)
            status = PlayerAnimStatus.Down;
        else if (ps.isOnGround && ps.isWalking && ps.isDown && !ps.isLookUp && !ps.isShooting)
            status = PlayerAnimStatus.DownWalk;
        else if (ps.isOnGround && !ps.isWalking && !ps.isDown && !ps.isLookUp && ps.isShooting)
            status = PlayerAnimStatus.IdleShoot;
        else if (ps.isOnGround && !ps.isWalking && !ps.isDown && ps.isLookUp && ps.isShooting)
            status = PlayerAnimStatus.IdleUpShoot;
        else if (ps.isOnGround && ps.isWalking && !ps.isDown && !ps.isLookUp && ps.isShooting)
            status = PlayerAnimStatus.WalkShoot;
        else if (ps.isOnGround && ps.isWalking && !ps.isDown && ps.isLookUp && ps.isShooting)
            status = PlayerAnimStatus.WalkUpShoot;
        else if (!ps.isOnGround && !ps.isDown && !ps.isLookUp && ps.isShooting)
            status = PlayerAnimStatus.JumpShoot;
        else if (!ps.isOnGround && ps.isLookUp && ps.isShooting)
            status = PlayerAnimStatus.JumpUpShoot;
        else if (!ps.isOnGround && ps.isDown && !ps.isLookUp && ps.isShooting)
            status = PlayerAnimStatus.JumpDownShoot;
        else if (ps.isOnGround && !ps.isWalking && ps.isDown && !ps.isLookUp && ps.isShooting)
            status = PlayerAnimStatus.DownShoot;
        else if (ps.isOnGround && ps.isWalking && ps.isDown && !ps.isLookUp && ps.isShooting)
            status = PlayerAnimStatus.DownWalkShoot;

        else if (ps.isOnGround)
            status = PlayerAnimStatus.Idle;
        else if (!ps.isOnGround)
            status = PlayerAnimStatus.Jump;

    }


    /**
     * 角色朝向改变
     */
    private void changePlayerOrientation() {
        if (Input.GetAxis("Horizontal") > 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        } else if (Input.GetAxis("Horizontal") < 0) {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }


    /**
     *  根据状态播放对应动画
     */
    private void playAnim() {
        foreach (var p in PlayerAnim) {
            if (p.gameObject.name == status.ToString())
                p.gameObject.SetActive(true);
            else
                p.gameObject.SetActive(false);
        }
    }
    #endregion
}
