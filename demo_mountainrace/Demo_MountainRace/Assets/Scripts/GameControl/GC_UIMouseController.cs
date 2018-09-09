using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//[RequireComponent(typeof(UnityEngine.EventSystems.EventTrigger))]   //[RequireComponent(typeof(xxx))]是脚本依赖，xxx为需要依赖的组件/脚本，当此脚本挂上游戏物体对象时，会自动挂上xxx组件/脚本
public class GC_UIMouseController : MonoBehaviour {

    /// <summary>
    /// 模拟按键
    /// </summary>
    /// <param name="bvk">虚拟键值 ESC键对应的是27</param>
    /// <param name="bScan">0</param>
    /// <param name="dwFlags">0为按下，1按住，2释放</param>
    /// <param name="dwExtraInfo">0</param>
    [DllImport("user32.dll", EntryPoint = "keybd_event")]       // 注：android手机设备，不支持导入dll动态链接库
    public static extern void my_key_event(byte bvk, byte bScan, int dwFlags, int dwExtraInfo);


    #region 代码查找按钮，添加accelerateButton按钮的监听事件，Button按钮对象上需要EventTrigger组件
    private Button accelerateButton;

    private void Start() {
        addAccButtonEvent();
    }


    /**
     * 绑定accelerateButton按钮事件
     * 注：需要为按钮添加Event Trigger组件
     */
    private void addAccButtonEvent() {
        accelerateButton = GameObject.Find("accelerateButton").GetComponent<Button>();

        // 给按钮添加按下松起事件
        // 添加形式1：
        // 添加按钮按下事件
        EventTrigger trigger = accelerateButton.gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry1 = new EventTrigger.Entry { // 直接在构造函数中设置
            eventID = EventTriggerType.PointerDown, // 按下
            callback = new EventTrigger.TriggerEvent()
        };
        entry1.callback.AddListener(AccelerateCar);      // 添加函数
        trigger.triggers.Add(entry1);
        // 添加形式2：
        // 添加按钮松开事件
        EventTrigger.Entry entry2 = new EventTrigger.Entry();    // 下两行单独设置
        entry2.eventID = EventTriggerType.PointerUp;   // 抬起
        entry2.callback = new EventTrigger.TriggerEvent();
        entry2.callback.AddListener(delegate { my_key_event(68, 0, 2, 0); });    // 添加委托
        trigger.triggers.Add(entry2);
    }


    /**
     * 被绑定的事件
     */
    private void AccelerateCar(BaseEventData eventData) {
        my_key_event(68, 0, 0, 0);
    }
    #endregion




    #region 直接在按钮组件上设置事件执行的函数，Button按钮对象上需要EventTrigger组件
    public void brakeCarDown() {
        my_key_event(65, 0, 0, 0);
    }


    public void brakeCarUp() {
        my_key_event(65, 0, 2, 0);
    }
    #endregion
}
