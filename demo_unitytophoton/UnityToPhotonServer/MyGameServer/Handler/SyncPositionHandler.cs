using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Tools;
using Photon.SocketServer;

namespace MyGameServer.Handler {
    class SyncPositionHandler : HandlerBase {

        public SyncPositionHandler() {
            OpCode = OperationCode.SyncPosition;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer) {
            float x = (float)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.X);
            float y = (float)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Y);
            float z = (float)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Z);
            peer.x = x; peer.y = y; peer.z = z;
            MyGameServer.log.Info(peer.username + " 位置：" + peer.x + " " + peer.y + " " + peer.z);
        }
    }
}
