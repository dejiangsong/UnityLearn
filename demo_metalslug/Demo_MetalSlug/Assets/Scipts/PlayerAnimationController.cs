using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

    public SpriteRenderer UpRenderer;   // 上半身的渲染器
    public SpriteRenderer DownRenderer;   // 下半身的渲染器
    public Sprite[] UpSprites;  // 上半身精灵数组
    public Sprite[] DownSprites;    // 下半身精灵数组
    [Range(1, 60)]
    public int UpAnimSpeed = 10;    // 上半身动画速度，10帧/1秒
    [Range(1, 60)]
    public int DownAnimSpeed = 10;    // 下半身动画速度，10帧/1秒

    private float upAnimTimeInterval = 0;     // 上半身动画间隔
    private float downAnimTimeInterval = 0;     // 下半身动画间隔
    private int upIndex = 0;     // 上半身索引
    private float upTimer = 0f;     // 上半身计时器
    private int downIndex = 0;     // 下半身索引
    private float downTimer = 0f;     // 下半身计时器


    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        changeSpeed(UpAnimSpeed, DownAnimSpeed);      // 设置上下身的播放速度

        upTimer += Time.deltaTime;      // 上半身计时器计时
        downTimer += Time.deltaTime;      // 下半身计时器计时
        if (upTimer > upAnimTimeInterval) {    // 上半身达到计时，播放下一帧
            upTimer -= upAnimTimeInterval;    // 重置计时器(直接归零会导致累积误差)
            upIndex = ++upIndex % UpSprites.Length;     // 上半身索引增加
            UpRenderer.sprite = UpSprites[upIndex];     // 播放下一帧
        }
        if (downTimer > downAnimTimeInterval) {    // 上半身达到计时，播放下一帧
            downTimer -= downAnimTimeInterval;    // 重置计时器(直接归零会导致累积误差)
            downIndex = ++downIndex % DownSprites.Length;     // 上半身索引增加
            DownRenderer.sprite = DownSprites[downIndex];     // 播放下一帧
        }
    }


    #region util
    /**
     * 改变速度
     */
    private void changeSpeed(int upSpeed, int downSpeed) {
        upAnimTimeInterval = 1f / upSpeed;       // 得到每一帧的间隔
        downAnimTimeInterval = 1f / downSpeed;       // 得到每一帧的间隔
    }
    #endregion
}
