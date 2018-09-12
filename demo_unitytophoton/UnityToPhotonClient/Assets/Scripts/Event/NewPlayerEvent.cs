using System.Collections;
using System.Collections.Generic;
using Common;
using Common.Tools;
using ExitGames.Client.Photon;
using UnityEngine;

public class NewPlayerEvent : EventBase {

    public NewPlayerEvent() {
        EventCode = EventCode.NewPlayer;
    }

    public override void OnEvent(EventData eventData) {
        string username = (string)DictTool.GetValue<byte, object>(eventData.Parameters, (byte)ParameterCode.Username);
        GameObject.Find("Sync Manager").GetComponent<SyncManager>().SendMessage("OnNewPlayerEvent", username);
    }
}
