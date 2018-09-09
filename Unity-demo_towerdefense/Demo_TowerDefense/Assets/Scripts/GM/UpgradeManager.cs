using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour {

    public GameObject UpgradeCanvas;

    private GameObject selectTurret;        // 得到被选中的炮塔
    private int moneyValue;                 // 得到钱有多少
    private TurretData turretData;          // 得到炮塔数据


    // Update is called once per frame
    void Update() {

    }


    /// <summary>
    /// 显示面板
    /// </summary>
    public void showPanel(Vector3 position, bool isDisableUpgrade = false) {
        Vector3 v = position;
        v.y = v.y * 2f + 3;
        UpgradeCanvas.transform.position = v;
        UpgradeCanvas.SetActive(false);     // 先禁用，使动画状态机初始化实现动画在切换其它炮塔时重新播放
        StopCoroutine("disablePanel");      // 停止掉可能未完成的关闭激活面板的协程
        UpgradeCanvas.SetActive(true);              // 显示面板
        Button button = UpgradeCanvas.transform.Find("background").Find("upgradeButton").GetComponent<Button>();
        button.interactable = !isDisableUpgrade;        // 是否禁用升级按钮
    }


    /// <summary>
    /// 隐藏面板
    /// </summary>
    public void hindPanel() {
        StartCoroutine("disablePanel");
    }
    private IEnumerator disablePanel() {
        UpgradeCanvas.GetComponent<Animator>().SetTrigger("hind");        // 播放动画
        yield return new WaitForSeconds(0.5f);
        UpgradeCanvas.SetActive(false);
    }


    #region UI事件
    /// <summary>
    /// 升级炮塔
    /// </summary>
    public void UpgradeTurret() {
        TowerSpawn selectedTowerSpawn = gameObject.GetComponent<BuildManager>().SelectedTowerSpawn;       // 从建筑管理器中，得到当前被选中的炮台
        turretData = selectedTowerSpawn.TurretData;             // 从炮台上得到炮塔数据
        selectTurret = selectedTowerSpawn.TurretGo;             // 从炮台得到上面的炮塔
        moneyValue = gameObject.GetComponent<BuildManager>().GetMoney();                       // 从建筑管理器中，得到有多少钱

        // 升级行为以调整系数为间接调整属性，避免累积
        if (moneyValue > turretData.UpgradeCost) {                // 有钱升级，则进行升级行为
            gameObject.GetComponent<BuildManager>().DecreaseMoney(turretData.UpgradeCost);      // 先花掉钱
            turretData.TotalCost += turretData.UpgradeCost;       // 升级总花费增加
            turretData.UpgradeCount++;                              // 升级次数计数增加
            Turret turret = selectTurret.GetComponent<Turret>();        // 得到炮塔上的Turret脚本，用来升级属性
            turret.AttackRateScale *= 0.9f;                  // 每次升级攻速提升10%
            turret.AttackPowerScale *= 1.2f;            // 提升20%攻击力
            turret.GetComponent<SphereCollider>().radius += 3f;   // 升级3点攻击距离

            // TODO 达到一定升级次数，进化
        }
    }


    /// <summary>
    /// 拆除炮塔
    /// </summary>
    public void BreakTurret() {
        TowerSpawn selectedTowerSpawn = GetComponent<BuildManager>().SelectedTowerSpawn;       // 从建筑管理器中，得到当前被选中的炮台
        selectTurret = selectedTowerSpawn.TurretGo;             // 从炮台得到上面的炮塔
        turretData = selectedTowerSpawn.TurretData;             // 从炮台上得到炮塔数据

        gameObject.GetComponent<BuildManager>().AddMoney((int)(turretData.TotalCost * 0.7));        // 拆了炮塔加钱，加的钱是拆炮塔的70%
        Destroy(selectTurret);          // 销毁炮塔
        hindPanel();                                // 拆完，隐藏面板
    }
    #endregion
}
