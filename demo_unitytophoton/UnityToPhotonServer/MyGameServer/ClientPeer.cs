using System;
using System.Collections.Generic;
using System.Text;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using Common;
using MyGameServer.Handler;

namespace MyGameServer {
    // 继承ClientPeer作为客户端连接的对象，ClientPeer继承PeerBase
    public class ClientPeer : Photon.SocketServer.ClientPeer {

        public string username;     // 角色名
        public float x, y, z;       // 角色坐标位置


        public ClientPeer(InitRequest initRequest) : base(initRequest) {
        }

        // 客户端断开连接的处理
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail) {
            MyGameServer.Instance.PeerList.Remove(this);        // 从已连接的列表中移出自身
            MyGameServer.log.Info(username + "已断开连接");
        }

        // 客户端请求的处理
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters) {
            Dictionary<OperationCode, HandlerBase> handlerDict = MyGameServer.Instance.HandlerDict;        // 得到MyGameServer类中定义的handler字典
            HandlerBase handler = null;
            bool isGet = handlerDict.TryGetValue((OperationCode)operationRequest.OperationCode, out handler);      // 从字典通过操作码取得对应的handler
            if (isGet) {
                handler.OnOperationRequest(operationRequest, sendParameters, this);       // 调用对应的操作码的handler处理请求
            } else {
                MyGameServer.log.Info("没有找到与操作码对应的handler去处理");
            }

            // 不使用字典和操作码进行处理分发
            //switch (operationRequest.OperationCode) {       // 通过客户端传递过来的请求operationRequest的OperationCode区分请求
            //    case 1:
            //        MyGameServer.log.Info("收到了1个请求");

            //        Dictionary<byte, object> data1 = operationRequest.Parameters;
            //        object health;
            //        object msg;
            //        data1.TryGetValue(11, out health);
            //        data1.TryGetValue(12, out msg);
            //        MyGameServer.log.Info("得到的数据是" + health + " " + msg);

            //        Dictionary<byte, object> data2 = new Dictionary<byte, object>();
            //        data2.Add(11,80);
            //        data2.Add(12,"Yes");
            //        OperationResponse opResponse = new OperationResponse(1);        // 可以指定参数operationCode，作为客户端判断是那条请求得到的响应（通常应与请求的operationRequest.OperationCode一致）
            //        opResponse.SetParameters(data2);       // 设置响应传回的数据字典
            //        SendOperationResponse(opResponse, sendParameters);      // 响应客户端的请求，sendParameters为一些传送数据时的参数设置（这里直接使用客户端发送时用的设置参数）

            //        EventData ed = new EventData(101);      // 创建一个事件数据，指定eventCode作为区分是哪个事件，ed可以SetParameters指定字典数据（类似请求响应）
            //        SendEvent(ed, new SendParameters());        // 发送事件，可以在程序的任意地方发送事件
            //        break;
            //    default:
            //        break;
            //}
        }
    }
}
