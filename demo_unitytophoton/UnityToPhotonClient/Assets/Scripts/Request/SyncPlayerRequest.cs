using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Common;
using Common.Tools;
using ExitGames.Client.Photon;
using UnityEngine;

public class SyncPlayerRequest : RequestBase {

    public SyncPlayerRequest() {
        OpCode = OperationCode.SyncPlayer;
    }

    public override void DefaultRequest() {
        PhotonEngine.Peer.OpCustom((byte)OpCode, null, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse) {
        // 得到序列化的用户名列表
        string usernameListString = (string)DictTool.GetValue<byte, object>(operationResponse.Parameters, (byte)ParameterCode.UsernameList);

        // 反序列化用户名列表
        using (StringReader reader = new StringReader(usernameListString)) {
            XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
            List<string> usernameList = (List<string>)serializer.Deserialize(reader);
            GetComponent<SyncManager>().SendMessage("OnSyncPlayerResponse", usernameList);        // 提供用户列表，交予其它类处理UI
        }
    }
}
