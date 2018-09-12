using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;      // Photon客户端命名空间
using Common;

public class PhotonEngine : MonoBehaviour, IPhotonPeerListener {

    public static PhotonPeer Peer {
        get {
            return peer;
        }
    }

    public static PhotonEngine Instance;       // 单例模式，便于在其他地方访问
    private static PhotonPeer peer;        // 作为客户端的实例对象与服务器端连接
    private Dictionary<OperationCode, RequestBase> RequestDict = new Dictionary<OperationCode, RequestBase>();     // 所有请求的字典，实现请求的处理分发    
    private Dictionary<EventCode, EventBase> EventDict = new Dictionary<EventCode, EventBase>();     // 所有事件的字典，实现事件的处理分发    


    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else if (Instance != this) {      // 使游戏中只存在1个PhotonEngine游戏物体
            Destroy(this.gameObject);
            return;
        }
    }

    // Use this for initialization
    void Start() {
        // 通过PhotonPeerListener实现与服务器端信息交换的事件处理
        //      第一个参数指定监听自己类的IPhotonPeerListener接口
        //      第二个参数指定通讯协议，Udp速度快，Tcp比较稳定
        peer = new PhotonPeer(this, ConnectionProtocol.Udp);
        // 连接
        //      第一个参数为连接的IP:端口（IP：此处为本机的Photon服务器；端口：为配置文件PhotonServer.config中UDPListeners的Port）
        //      第二个参数为应用名（配置文件PhotonServer.config中Application的Name）
        peer.Connect("127.0.0.1:5055", "MyGame1");
    }

    // Update is called once per frame
    void Update() {
        peer.Service();     // 启动服务
    }

    private void OnDestroy() {
        if (peer != null && peer.PeerState == PeerStateValue.Connected)     // 当脚本销毁时，如果peer不为空且连接状态处于连接时，断开连接
            peer.Disconnect();
    }

    #region 请求字典的添加移除/事件字典的添加移除
    public void AddRequest(RequestBase request) {
        RequestDict.Add(request.OpCode, request);
    }

    public void RemoveRequest(RequestBase request) {
        RequestDict.Remove(request.OpCode);
    }

    public void AddEvent(EventBase e) {
        EventDict.Add(e.EventCode, e);
    }

    public void RemoveEvent(EventBase e) {
        EventDict.Remove(e.EventCode);
    }
    #endregion

    #region 实现IPhotonPeerListener接口
    public void DebugReturn(DebugLevel level, string message) {
        Debug.Log("服务器未响应！");
    }

    /// <summary>
    /// 服务器端向客户端发起事件的处理方法
    /// 注：OnOperationResponse()需要客户端请求后得到响应信息，而OnEvent()是服务器端主动发起信息
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEvent(EventData eventData) {
        EventCode eventCode = (EventCode)eventData.Code;
        EventBase e = null;
        bool isGet = EventDict.TryGetValue(eventCode, out e);       //从事件字典中取得响应事件码的事件

        if (isGet) {
            e.OnEvent(eventData);
        } else {
            Debug.Log("没有找到对应的事件处理对象");
        }
    }

    /// <summary>
    /// 客户端向服务器端发起请求后，得到响应的处理方法
    /// </summary>
    /// <param name="operationResponse"></param>
    public void OnOperationResponse(OperationResponse operationResponse) {
        OperationCode opCode = (OperationCode)operationResponse.OperationCode;      // 取得响应的操作码
        RequestBase request = null;
        bool isGet = RequestDict.TryGetValue(opCode, out request);       // 从请求字典中通过请求码的到相应的请求

        if (isGet) {
            request.OnOperationResponse(operationResponse);
        } else {
            Debug.Log("没有找到对应的响应处理对象");
        }
    }

    public void OnStatusChanged(StatusCode statusCode) {
        Debug.Log(statusCode);
    }
    #endregion
}
