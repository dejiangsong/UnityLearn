using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public float HP;
    public float MaxHP = 100f;

    private Slider hpSlider;


    // Use this for initialization
    void Start() {
        HP = MaxHP;
        hpSlider = transform.Find("HP Canvas").Find("HP Slider").GetComponent<Slider>();
        hpSlider.value = HP / MaxHP;        // 设置血量长度显示
    }


    /// <summary>
    /// 接受伤害减血
    /// </summary>
    /// <param name="value"></param>
    public void TakeDamage(float value) {
        HP -= value;        // 减血

        if (HP <= 0) {      // 没血了，就挂掉
            this.gameObject.SendMessage("dealDie");     // 通知该游戏物体，处理死亡消息
            GameObject.Destroy(this.gameObject);
        }

        updateHpText();     // 更新hp显示
    }


    /// <summary>
    /// 更新Hp文本显示
    /// </summary>
    private void updateHpText() {
        hpSlider.value = HP / MaxHP;
    }
}
