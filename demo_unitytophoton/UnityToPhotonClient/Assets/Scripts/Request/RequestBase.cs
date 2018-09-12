using UnityEngine;
using Common;       // 引用自定义的公共类(.net framework 3.5)，包含操作符的枚举
using ExitGames.Client.Photon;

/// <summary>
/// 需要发起请求/处理响应的类，继承此类
/// 包含：请求的发起、发起请求后服务器的响应处理
/// 注：需要有PhotonEngine的静态对象
/// </summary>
public abstract class RequestBase : MonoBehaviour {
    
    [HideInInspector]
    public OperationCode OpCode;                    // 操作码标识（在子类的构造方法中设置）

    public abstract void DefaultRequest();          // 发起请求的默认方法（可以在任意需要的地方调用）
    public abstract void OnOperationResponse(OperationResponse operationResponse);     // 处理响应的方法（设置PhotonEngine.cs中的请求字典后，在它的OnOperationResponse()方法中区分操作码，以分发此响应的处理；会自动被分发处理）


    public virtual void Start() {
        PhotonEngine.Instance.AddRequest(this);         // 添加自身到Request字典
    }

    public void OnDestroy() {
        PhotonEngine.Instance.RemoveRequest(this);      // 从Request字典中移出自身
    }
}
