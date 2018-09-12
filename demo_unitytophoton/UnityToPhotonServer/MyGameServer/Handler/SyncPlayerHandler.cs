using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Common;
using Photon.SocketServer;

namespace MyGameServer.Handler {
    class SyncPlayerHandler : HandlerBase {

        public SyncPlayerHandler() {
            OpCode = OperationCode.SyncPlayer;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer) {
            // 取得所有已经登录（在线玩家）的用户名
            List<string> usernameList = new List<string>();
            foreach (var tempPeer in MyGameServer.Instance.PeerList) {
                if (!string.IsNullOrEmpty(tempPeer.username) && tempPeer != peer) {
                    usernameList.Add(tempPeer.username);
                }
            }

            // 序列化xml
            StringWriter sw = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
            serializer.Serialize(sw, usernameList);
            sw.Close();
            string usernameListString = sw.ToString();

            // 响应发送列表到客户端
            Dictionary<byte, object> data = new Dictionary<byte, object>();
            data.Add((byte)ParameterCode.UsernameList, usernameListString);
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            response.SetParameters(data);
            peer.SendOperationResponse(response, sendParameters);


            // 通知其它客户端，有新的客户端加入
            foreach (var tempPeer in MyGameServer.Instance.PeerList) {
                if(string.IsNullOrEmpty(tempPeer.username) == false && tempPeer != peer) {
                    EventData ed = new EventData((byte)EventCode.NewPlayer);
                    Dictionary<byte, object> data2 = new Dictionary<byte, object>();
                    data2.Add((byte)ParameterCode.Username,peer.username);      // 发送的数据为新加入的客户端用户名
                    ed.SetParameters(data2);
                    tempPeer.SendEvent(ed,sendParameters);
                }
            }
        }
    }
}
