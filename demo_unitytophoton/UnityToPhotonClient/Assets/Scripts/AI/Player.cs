using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [HideInInspector]
    public string username;
    public GameObject PlayerPrefab;
    public bool isLocalPlayer;

    private SyncPlayerRequest syncPlayerRequest;        // 同步新增的玩家请求


    // Use this for initialization
    void Start() {
        if (isLocalPlayer) {        // 本地玩家需要激活的组件
            username = PlayerPrefs.GetString("username");       // 设置名字
            gameObject.GetComponent<Renderer>().material.color = Color.black;      // 改变颜色用于区分
            syncPlayerRequest = GameObject.Find("Sync Manager").GetComponent<SyncPlayerRequest>();      // 得到同步管理器上同步玩家的脚本
        }

        if (isLocalPlayer) {        // 本地玩家发起的同步请求
            syncPlayerRequest.DefaultRequest();     // 新登录的玩家发起同步角色的请求
        }
    }

    private void OnDestroy() {
        SyncManager.Instance.RemovePlayer(username);
    }
}
