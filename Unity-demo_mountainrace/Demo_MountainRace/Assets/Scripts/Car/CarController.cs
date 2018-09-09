using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    public WheelJoint2D F_wheel;        // 轮子
    public WheelJoint2D B_wheel;        // 轮子
    public float BaseMotorSpeed = 480;       // 最低基本转速（非实际转速，后面有控制按键+deltaTime的缩小）
    public float MaxMotorSpeed = 3000f;         // 最大转速（实际转速）
    public float Speed = 60f;
    public float SpeedScale = 1f; // 速度调整系数

    private JointMotor2D F_jm;      // 电机
    private JointMotor2D B_jm;      // 电机
    private float touchSpeedScale = 0f;  // 触屏实现加速过程的比例变量


    private void Awake() {
        F_jm = F_wheel.motor;       // 得到轮子的电机
        B_jm = B_wheel.motor;       // 得到轮子的电机
    }


    // Update is called once per frame
    void Update() {
        if (Input.touchCount > 0) {
            touchMove();
        } else {
            keyMove();
        }
    }


    /**
     * 触屏控制
     */
    private void touchMove() {
        float tmpSpeed = (BaseMotorSpeed / 0.016f + Speed * SpeedScale * 360) * touchSpeedScale * Time.deltaTime;   // 设置电机速度
        if (Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(0).phase == TouchPhase.Moved) {
            tmpSpeed = Mathf.Abs(tmpSpeed) < MaxMotorSpeed ? tmpSpeed : MaxMotorSpeed;
            CancelInvoke("slowdown");   //触屏时，关闭减速，避免同时影响
            if (Input.GetTouch(0).position.x < Screen.width / 2) {  // 触屏左边，速度取反
                tmpSpeed *= -1;
            }
            touchSpeedScale += Time.deltaTime;   // 递增计数
        } else if (Input.GetTouch(0).phase == TouchPhase.Ended) {
            touchSpeedScale = 0;
        }

        updateMotor(tmpSpeed);  //更新电机
    }


    /**
     * 按键控制
     */
    private void keyMove() {
        float ax = Input.GetAxis("Horizontal");     // 输入
        float tmpSpeed = (BaseMotorSpeed / 0.016f + Speed * SpeedScale * 360) * ax * Time.deltaTime;   // 设置电机速度
        if (tmpSpeed > 0) { // 速度是否超过最大值、速度方向判断
            tmpSpeed = Mathf.Abs(tmpSpeed) < MaxMotorSpeed ? tmpSpeed : MaxMotorSpeed;
        } else {
            tmpSpeed = Mathf.Abs(tmpSpeed) < MaxMotorSpeed ? tmpSpeed : MaxMotorSpeed * -1;
        }

        updateMotor(tmpSpeed);  //更新电机
    }



    #region 公共函数
    /**
     * 更新轮子的电机属性
     */
    private void updateMotor(float speed) {
        F_jm.motorSpeed = B_jm.motorSpeed = speed;
        // 每次都需要电机JointMotor2D.motor的设置(速度)改动，重新赋给轮子WheelJoint2D.motor
        F_wheel.motor = F_jm;
        B_wheel.motor = B_jm;
    }
    #endregion
}
