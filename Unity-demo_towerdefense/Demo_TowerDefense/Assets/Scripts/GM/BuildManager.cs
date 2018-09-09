using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

    public TurretData StandardTurretData;
    public TurretData MissileTurretData;
    public TurretData LaserTurretData;
    [HideInInspector]
    public TowerSpawn SelectedTowerSpawn;       // 保存被选中的炮台

    private TurretData SelectedCurrentTurretData;   // 当前选择的炮台图标
    private Money money;                        // 金钱值
    private UpgradeManager upgradeManager;      // 升级管理

    // Use this for initialization
    void Start() {
        SelectedCurrentTurretData = null;       // 初始化选择
        money = new Money(1000);                // 初始化金钱
        upgradeManager = GameObject.Find("Game Manager").GetComponent<UpgradeManager>();     // 初始化升级管理器
    }


    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {      // 鼠标左键按下
            if (EventSystem.current.IsPointerOverGameObject() == false) {       // 如果鼠标没有点击到UI
                // 射线检测
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);        // 生成一个鼠标点击位置的射线
                RaycastHit hit;     // 用以存储射线检测的结果
                bool isCollider = Physics.Raycast(ray, out hit, 1000f, LayerMask.GetMask("TowerSpawn"));     // 射线检测
                // 是否建筑
                if (isCollider) {
                    SelectedTowerSpawn = hit.collider.GetComponent<TowerSpawn>();        // 如果碰撞到TowerSpawn，得到碰撞到的游戏物体
                    if (SelectedTowerSpawn.TurretGo == null && SelectedCurrentTurretData != null) {    // 炮台上为空，且选中要建的炮塔图标，可以创建
                        if (money.Value > SelectedCurrentTurretData.Cost) {      // 钱够才可以创建
                            money.Value -= SelectedCurrentTurretData.Cost;      // 花掉钱
                            updateMoneyText();           // 更新金钱的UI文本
                            SelectedTowerSpawn.BuildTurret(SelectedCurrentTurretData.TurretPrefab, SelectedCurrentTurretData);     // 调用炮台的建筑函数
                            SelectedCurrentTurretData = null;       // 建完了就置空选择，避免误点
                        } else {        // 没钱，就提示钱不够
                            flashMoneyText();
                        }
                    } else if (SelectedTowerSpawn.TurretGo != null) {        // 炮台上不为空，可以升级/拆毁炮塔
                        if (money.Value > SelectedTowerSpawn.TurretData.UpgradeCost)                    // 判断是否钱够升级用
                            upgradeManager.showPanel(SelectedTowerSpawn.transform.position, false);     // 显示升级/拆除面板 - 钱够
                        else
                            upgradeManager.showPanel(SelectedTowerSpawn.transform.position, true);      // 显示升级/拆除面板 - 钱不够
                    }
                } else {
                    SelectedCurrentTurretData = null;       // 点到空地就置空选择
                    upgradeManager.hindPanel();             // 隐藏升级/拆除面板
                }
            }
        }
    }


    #region 公共函数
    public int GetMoney() {
        return money.Value;
    }


    public void DecreaseMoney(int value) {
        money.Value -= value;
        updateMoneyText();
    }


    public void AddMoney(int value) {
        money.Value += value;
        updateMoneyText();
    }
    #endregion


    #region UI事件
    public void OnStandardSelected(bool isOn) {
        if (isOn) {
            SelectedCurrentTurretData = StandardTurretData;
        }
    }


    public void OnMissileSelected(bool isOn) {
        if (isOn) {
            SelectedCurrentTurretData = MissileTurretData;
        }
    }


    public void OnLaserSelected(bool isOn) {
        if (isOn) {
            SelectedCurrentTurretData = LaserTurretData;
        }
    }
    #endregion

    #region UI更新
    /// <summary>
    /// 更新money文本
    /// </summary>
    /// <param name="value"></param>
    private void updateMoneyText() {
        Text text = GameObject.Find("moneyText").GetComponent<Text>();      // 获取到UI的金钱文本显示组件
        text.text = "$" + money.Value;
    }


    /// <summary>
    /// 钱不够，闪烁money文本
    /// </summary>
    private void flashMoneyText() {
        Animator animator = GameObject.Find("moneyText").GetComponent<Animator>();
        animator.SetTrigger("flash");
    }

    #endregion
}
