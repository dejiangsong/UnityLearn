using Common;
using Photon.SocketServer;

/// <summary>
/// 需要处理请求的类，继承此类
/// </summary>
namespace MyGameServer.Handler {
    public abstract class HandlerBase {
        public OperationCode OpCode;        // 用作区分处理请求的处理者handler去处理

        public abstract void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer);      // 处理请求，通过peer知道是哪个客户端传来的请求
    }
}
