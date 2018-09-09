using DragonBones;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject Robot1;

    private UnityArmatureComponent robot1;
    private string[] animations = { "stand", "run", "jump", "attack" };
    private int index = 0;
    private float timeScale = 0.1f;

    ListenerDelegate<EventObject> startPlayLD;   // 用于动画事件的监听器委托
    ListenerDelegate<EventObject> completePlayLD;   // 用于动画事件的监听器委托
    ListenerDelegate<EventObject> attackFrameLD;   // 用于动画事件的监听器委托
    ListenerDelegate<EventObject> attackSoundLD;   // 用于动画事件的监听器委托


    // Use this for initialization
    void Start() {
        // 获得UnityArmatureComponent组件
        robot1 = Robot1.GetComponent<UnityArmatureComponent>();

        // 监听动画事件
        // 方法一：直接添加委托，适用于小段代码
        //robot1.AddEventListener(EventObject.START, delegate { print("开始播放动画了"); });
        // 方法二：使用AddEventListener指定的类型创建ListenerDelegate<EventObject>变量委托，添加需要执行的函数
        startPlayLD += this.startPlay;
        robot1.AddEventListener(EventObject.START, startPlayLD);    // 监听动画开始事件
        completePlayLD += this.completePlay;
        robot1.AddEventListener(EventObject.COMPLETE, completePlayLD);    // 监听动画结束事件(循环动画时，使用LOOP_COMPLETE)
        attackFrameLD += this.attackFrame;
        robot1.AddEventListener(EventObject.FRAME_EVENT, attackFrameLD);    // 监听动画某一帧事件
        attackSoundLD += attackSound;
        robot1.AddEventListener(EventObject.SOUND_EVENT, attackSoundLD);    // 监听动画声音事件
    }


    // Update is called once per frame
    void Update() {

    }

    #region UI按钮事件
    public void Play() {
        if (robot1.animation.isPlaying)
            robot1.animation.Stop();
        else
            robot1.animation.Play(animations[index]);
    }


    public void ChangeAnimal() {
        index = ++index % animations.Length;
        robot1.animation.Play(animations[index]);
    }


    public void ChangeTime() {
        print("timeScale:" + timeScale);
        robot1.animation.timeScale = timeScale;
        timeScale *= 2;
        if (timeScale > 5) {
            timeScale = 0.1f;
        }
    }
    #endregion


    #region 动画帧事件
    void startPlay(string str, EventObject e) {
        print("开始播放动画了");
        print("str:" + str);
    }


    void completePlay(string str, EventObject e) {
        print("结束播放动画了");
        print("str:" + str);
    }


    void attackFrame(string str, EventObject e) {
        print("实际攻击到的帧数");
        print("str:" + str);
        print("此帧事件的字符串数据：" + e.data.GetString());
        print("此帧事件的浮点数数据：" + e.data.GetFloat());
        e.data.AddString("附加的字符串1");
        print("此帧事件的字符串数据（附加后，第1个）：" + e.data.GetString(0));
        print("此帧事件的字符串数据（附加后，第2个）：" + e.data.GetString(1));
    }


    void attackSound(string str, EventObject e) {
        print("播放了声音");
        print("str:" + str);
        Robot1.GetComponent<AudioSource>().Play();
    }
    #endregion


}
