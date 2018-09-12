using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using Common;
using Common.Tools;
using System.IO;
using System.Xml.Serialization;

public class SyncOthersPositionEvent : EventBase {

    public SyncOthersPositionEvent() {
        EventCode = EventCode.SyncOthersPosition;
    }

    public override void OnEvent(EventData eventData) {
        // 反序列化用户数据列表
        string playerDataListString = (string)DictTool.GetValue<byte, object>(eventData.Parameters,(byte)ParameterCode.PlayerDataList);
        using (StringReader reader = new StringReader(playerDataListString)) {
            XmlSerializer serializer = new XmlSerializer(typeof(List<PlayerData>));
            List<PlayerData> playerDataList = (List<PlayerData>)serializer.Deserialize(reader);

            GameObject.Find("Sync Manager").GetComponent<SyncManager>().SendMessage("OnSyncOthersPositionEvent", playerDataList);       // 交给同步管理的游戏物体处理
        }
    }
    
}
