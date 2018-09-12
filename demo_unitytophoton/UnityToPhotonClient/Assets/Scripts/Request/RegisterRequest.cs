using System.Collections;
using System.Collections.Generic;
using Common;
using ExitGames.Client.Photon;
using UnityEngine;

public class RegisterRequest : RequestBase {

    [HideInInspector]
    public string Username;
    [HideInInspector]
    public string Password;


    public RegisterRequest() {
        OpCode = OperationCode.Register;
    }

    public override void Start() {
        base.Start();
    }

    public override void DefaultRequest() {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.Username, Username);
        data.Add((byte)ParameterCode.Password, Password);

        PhotonEngine.Peer.OpCustom((byte)OpCode, data, true);       // 发起请求
    }

    public override void OnOperationResponse(OperationResponse operationResponse) {
        Debug.Log((ReturnCode)operationResponse.ReturnCode);
        gameObject.GetComponent<RegisterPanel>().SendMessage("OnRegisterResponseUI", (ReturnCode)operationResponse.ReturnCode);     // 提供响应的返回值，交予其它类处理UI
    }
}
