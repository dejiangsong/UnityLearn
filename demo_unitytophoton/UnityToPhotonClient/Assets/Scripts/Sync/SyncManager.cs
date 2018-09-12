using Common;
using Common.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncManager : MonoBehaviour {

    public static SyncManager Instance;
    public GameObject PlayerPrefab;

    public Dictionary<string, GameObject> playerDict = new Dictionary<string, GameObject>();       // 存放其它玩家的游戏物体列表


    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else if (Instance != this) {
            Destroy(this.gameObject);
            return;
        }
    }


    #region Commen
    /// <summary>
    /// 实例化新的玩家物体
    /// </summary>
    /// <param name="username"></param>
    private void addNewPlayer(string username) {
        GameObject go = GameObject.Instantiate(PlayerPrefab);
        Player player = go.GetComponent<Player>();
        player.isLocalPlayer = false;       // 其它玩家不设置为本地玩家
        player.username = username;         // 设置该玩家的名字
        if (go.GetComponent<SyncPositionRequest>() != null)
            Destroy(go.GetComponent<SyncPositionRequest>());
        AddPlayer(username, go);
    }

    /// <summary>
    /// 添加玩家物体
    /// </summary>
    /// <param name="username"></param>
    /// <param name="go"></param>
    public void AddPlayer(string username, GameObject go) {
        playerDict.Add(username, go);
    }

    /// <summary>
    /// 移除玩家物体
    /// </summary>
    /// <param name="username"></param>
    public void RemovePlayer(string username) {
        playerDict.Remove(username);
    }
    #endregion

    #region 同步请求响应的操作
    /// <summary>
    /// 同步已登录的玩家
    /// </summary>
    /// <param name="usernameList"></param>
    public void OnSyncPlayerResponse(List<string> usernameList) {
        // 从响应的得到的用户名列表中创建Player角色，并添加到角色字典中
        foreach (var username in usernameList) {
            addNewPlayer(username);
        }
    }

    /// <summary>
    /// 已登录的玩家同步新登录的玩家
    /// </summary>
    /// <param name="username"></param>
    public void OnNewPlayerEvent(string username) {
        addNewPlayer(username);
    }

    /// <summary>
    /// 同步其它客户端的位置事件处理
    /// </summary>
    /// <param name="playerDataList"></param>
    public void OnSyncOthersPositionEvent(List<PlayerData> playerDataList) {
        foreach (var playerData in playerDataList) {
            GameObject go = DictTool.GetValue<string, GameObject>(playerDict, playerData.Username);      // 从玩家字典中取得玩家游戏物体
            if (go != null)
                go.transform.position = new Vector3() { x = playerData.Position.X, y = playerData.Position.Y, z = playerData.Position.Z };      // 设置位置
        }
    }
    #endregion
}
