using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSpawn : MonoBehaviour {

    [HideInInspector]
    public GameObject TurretGo;
    public GameObject BuildEffectPrefab;
    [HideInInspector]
    public TurretData TurretData;      // 当前炮塔数据

    private Color oldColor;


    private void Start() {
        oldColor = this.GetComponent<Renderer>().material.color;
    }


    public void BuildTurret(GameObject turretPrefab, TurretData data) {
        if (turretPrefab == null)       // 炮塔预设体为空就返回
            return;

        TurretData = data;              // 存储炮塔数据

        // 建炮塔
        TurretGo = GameObject.Instantiate(turretPrefab, new Vector3(transform.position.x, transform.position.y * 2, transform.position.z), Quaternion.identity);
        GameObject effect = GameObject.Instantiate(BuildEffectPrefab, new Vector3(transform.position.x, transform.localScale.y, transform.position.z), Quaternion.identity);    // 显示建筑粒子特效
        Destroy(effect, 1f);
        TurretData.TotalCost = TurretData.Cost;     // 计算总花费
    }


    #region 鼠标移入事件
    private void OnMouseEnter() {       // 关闭physic中的对trigger的射线检测，使用鼠标射线检测游戏物体即可，避免炮塔攻击范围collider的影响
        if (TurretGo != null && EventSystem.current.IsPointerOverGameObject() == false)
            this.GetComponent<Renderer>().material.color = new Color(1f, 0.27f, 0.39f);
        else if (TurretGo == null && EventSystem.current.IsPointerOverGameObject() == false)      // 激活炮台条件：炮台上无炮塔 + 鼠标不在UI上
            this.GetComponent<Renderer>().material.color = new Color(0.47f, 0.78f, 0.31f);
    }


    private void OnMouseExit() {
        this.GetComponent<Renderer>().material.color = oldColor;
    }
    #endregion
}
